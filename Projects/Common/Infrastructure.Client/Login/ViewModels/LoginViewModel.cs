﻿using System.Configuration;
using System.Reflection;
using Common;
using FiresecAPI.Models;
using FiresecClient;
using Infrastructure.Client.Properties;
using Infrastructure.Common.Windows;
using Infrastructure.Common.Windows.ViewModels;

namespace Infrastructure.Client.Login.ViewModels
{
	internal class LoginViewModel : SaveCancelDialogViewModel
	{
		private PasswordViewType _passwordViewType;

		public LoginViewModel(ClientType clientType)
			: this(clientType, PasswordViewType.Connect)
		{
		}
		public LoginViewModel(ClientType clientType, PasswordViewType passwordViewType)
		{
			ClientType = clientType;
			_passwordViewType = passwordViewType;

			switch (_passwordViewType)
			{
				case PasswordViewType.Connect:
					UserName = Settings.Default.UserName;
					CanEditUserName = true;
					break;

				case PasswordViewType.Reconnect:
					UserName = string.Empty;
					CanEditUserName = true;
					break;

				case PasswordViewType.Validate:
					UserName = FiresecManager.CurrentUser.Login;
					CanEditUserName = false;
					break;
			}
			CanSavePassword = _passwordViewType != PasswordViewType.Validate;
			Password = Settings.Default.SavePassword ? Settings.Default.Password : string.Empty;
			SavePassword = Settings.Default.SavePassword;

			IsConnected = false;
			IsCanceled = false;
			Message = null;
			Sizable = false;
		}

		string _userName;
		public string UserName
		{
			get { return _userName; }
			set
			{
				_userName = value;
				OnPropertyChanged("UserName");
			}
		}
		string _password;
		[ObfuscationAttribute(Exclude = true)]
		public string Password
		{
			get { return _password; }
			set
			{
				_password = value;
				OnPropertyChanged("Password");
			}
		}
		private bool _savePassword;
		public bool SavePassword
		{
			get { return _savePassword; }
			set
			{
				_savePassword = value;
				OnPropertyChanged("SavePassword");
			}
		}
		bool _canEditUserName;
		public bool CanEditUserName
		{
			get { return _canEditUserName; }
			set
			{
				_canEditUserName = value;
				OnPropertyChanged("CanEditUserName");
			}
		}
		bool _canSavePassword;
		public bool CanSavePassword
		{
			get { return _canSavePassword; }
			set
			{
				_canSavePassword = value;
				OnPropertyChanged("CanSavePassword");
			}
		}

		public bool IsConnected { get; private set; }
		public bool IsCanceled { get; private set; }
		public string Message { get; private set; }
		public ClientType ClientType { get; private set; }

		protected override bool Save()
		{
			Close(true);
			IsCanceled = false;
			switch (_passwordViewType)
			{
				case PasswordViewType.Connect:
					DoConnect();
					break;
				case PasswordViewType.Reconnect:
					Message = FiresecManager.Reconnect(UserName, Password);
					break;
				case PasswordViewType.Validate:
					Message = HashHelper.CheckPass(Password, FiresecManager.CurrentUser.PasswordHash) ? null : "Валидация не пройдена";
					break;
			}
			IsConnected = string.IsNullOrEmpty(Message);
			if (CanSavePassword && IsConnected)
			{
				Settings.Default.UserName = UserName;
				Settings.Default.Password = SavePassword ? Password : string.Empty;
				Settings.Default.SavePassword = SavePassword;
				Settings.Default.Save();
			}
			return true;
		}
		protected override bool Cancel()
		{
			Message = null;
			return base.Cancel();
		}

		public override void OnClosed()
		{
			IsCanceled = true;
			base.OnClosed();
		}

		private void DoConnect()
		{
			ConnectionViewModel preLoadWindow = new ConnectionViewModel() { Title = "Соединение с сервером..." };
			DialogService.ShowWindow(preLoadWindow);
			Message = FiresecManager.Connect(ClientType, GetServerAddress(), UserName, Password);
			preLoadWindow.ForceClose();
		}

		private string GetServerAddress()
		{
			return ConfigurationManager.AppSettings["ServiceAddress"];
		}

		public enum PasswordViewType
		{
			Connect,
			Reconnect,
			Validate
		}
	}
}
