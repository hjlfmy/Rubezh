﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using Common;
using Infrastructure.Common.Windows;

namespace Infrastructure.Common
{
	public static class SingleLaunchHelper
	{
		public static bool KillRunningProcess(bool force = false)
		{
			string processName = Process.GetCurrentProcess().ProcessName;
			bool result = true;
			Process[] processes = null;

			try
			{
				processes = Process.GetProcessesByName(processName);

				if (processes.Count() > 1)
				{
					if (!force)
						result = MessageBoxService.ShowQuestion("Другой экзэмпляр программы уже запущен. Завершить?") == MessageBoxResult.Yes;

					if (result)
					{
						foreach (var process in processes)
						{
							if (process.Id == Process.GetCurrentProcess().Id)
								continue;

							if ((process != null) && (process.HasExited == false))
							{
								process.CloseMainWindow();
								//process.WaitForExit();
								process.Kill();
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				Logger.Error(e, "Исключение при вызове SingleLaunchHelper.KillRunningProcess");
			}

			return result;
		}
	}

	public static class MutexHelper
	{
		static Mutex Mutex { get; set; }

		public static bool IsNew(string mutexName)
		{
			bool isNew;
			Mutex = new Mutex(true, mutexName, out isNew);
			return isNew;
		}

		public static void KeepAlive()
		{
			if (Mutex != null)
				GC.KeepAlive(Mutex);
		}
	}

	public class DoubleLaunchLocker : IDisposable
	{
		private const int TIMEOUT = 300000;
		public string SignalId { get; private set; }
		public string WaitId { get; private set; }

		private EventWaitHandle _signalHandler;
		private EventWaitHandle _waitHandler;
		private bool _force = false;

		public DoubleLaunchLocker(string signalId, string waitId, bool force = false)
		{
			_force = force;
			SignalId = signalId;
			WaitId = waitId;
			bool isNew;
			_signalHandler = new EventWaitHandle(false, EventResetMode.AutoReset, signalId, out isNew);
			if (!isNew)
			{
				Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
				bool terminate = false;
				if (!force && !RequestConfirmation())
					terminate = true;
				if (!terminate)
				{
					_waitHandler = new EventWaitHandle(false, EventResetMode.AutoReset, waitId);
					if (_signalHandler.Set())
						terminate = !_waitHandler.WaitOne(TIMEOUT);
					else
						terminate = true;
				}
				if (terminate)
				{
					TryShutdown();
					ForceShutdown();
				}
				else
					Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;
			}
			ThreadPool.QueueUserWorkItem(WaitingHandler, waitId);
		}
		private bool RequestConfirmation()
		{
			return MessageBoxService.ShowConfirmation("Другой экзэмпляр программы уже запущен. Завершить?") == MessageBoxResult.Yes;
		}
		private void WaitingHandler(object startInfo)
		{
			try
			{
				_signalHandler.WaitOne();
				TryShutdown();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				ForceShutdown();
			}
			finally
			{
				_waitHandler = new EventWaitHandle(false, EventResetMode.AutoReset, (string)startInfo);
				_waitHandler.Set();
				ForceShutdown();
			}
		}
		private void TryShutdown()
		{
			if (!_force && Application.Current != null)
			{
				Application.Current.Dispatcher.InvokeShutdown();
				ApplicationService.DoEvents();
			}
		}
		private void ForceShutdown()
		{
			if (Application.Current == null || !Application.Current.Dispatcher.HasShutdownFinished)
				Environment.Exit(0);
		}

		#region IDisposable Members

		public void Dispose()
		{
			if (_signalHandler != null)
			{
				_signalHandler.Close();
				_signalHandler.Dispose();
			}
			if (_waitHandler != null)
			{
				_waitHandler.Close();
				_waitHandler.Dispose();
			}
		}

		#endregion
	}
}