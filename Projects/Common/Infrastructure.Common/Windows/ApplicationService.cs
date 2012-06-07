﻿using System.Windows;
using FiresecAPI.Models;
using Infrastructure.Common.Windows.ViewModels;
using Infrastructure.Common.Windows.Views;

namespace Infrastructure.Common.Windows
{
	public static class ApplicationService
	{
		public static User User { get; set; }
		public static void Run(ApplicationViewModel model)
		{
			WindowBaseView win = new WindowBaseView(model);
			model.Surface.Owner = null;
			model.Surface.ShowInTaskbar = true;
			model.Surface.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			if (Application.Current.Dispatcher.CheckAccess())
			{
				Application.Current.MainWindow = win;
				Application.Current.MainWindow.Show();
				Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
			}
			else
				win.Show();
		}
		public static void Run(ShellViewModel model)
		{
			Layout = new LayoutService(model);
			Run((ApplicationViewModel)model);
		}
		public static void ShutDown()
		{
			if (Application.Current.MainWindow != null)
				Application.Current.MainWindow.Close();
			Application.Current.Shutdown();
		}
		public static ILayoutService Layout { get; private set; }
	}
}
