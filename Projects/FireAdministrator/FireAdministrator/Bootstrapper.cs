﻿using System.Configuration;
using System.Linq;
using System.Windows;
using Common;
using Controls.MessageBox;
using FireAdministrator.ViewModels;
using FireAdministrator.Views;
using FiresecAPI.Models;
using FiresecClient;
using Infrastructure;
using Infrastructure.Common;
using Infrastructure.Events;
using System;

namespace FireAdministrator
{
	public class Bootstrapper
	{
		public void Initialize()
		{
			InitializeAppSettings();
			if (!SingleLaunchHelper.Check("FireAdministrator"))
			{
				Application.Current.Shutdown();
				System.Environment.Exit(1);
			}

			RegisterServices();
			ServiceFactory.ResourceService.AddResource(new ResourceDescription(GetType().Assembly, "DataTemplates/Dictionary.xaml"));

			var preLoadWindow = new PreLoadWindow();

			var loginViewModel = new LoginViewModel();
			var connectResult = false;
			if (ServiceFactory.AppSettings.AutoConnect)
				connectResult = loginViewModel.AutoConnect();
			if (connectResult == false)
			{
				connectResult = ServiceFactory.UserDialogs.ShowModalWindow(loginViewModel);
			}

			if (connectResult)
			{
				preLoadWindow.PreLoadText = "Инициализация компонент...";
				preLoadWindow.Show();

				FiresecManager.GetConfiguration();

				if (FiresecManager.CurrentUser.Permissions.Any(x => x == PermissionType.Adm_ViewConfig) == false)
				{
					MessageBoxService.Show("Нет прав на работу с программой");
					FiresecManager.Disconnect();
				}
				else
				{
					InitializeKnownModules();

					var ShellView = new ShellView();
					ServiceFactory.ShellView = ShellView;
					Application.Current.MainWindow = ShellView;
					Application.Current.MainWindow.Show();
				}
				preLoadWindow.Close();
			}
			else
			{
				preLoadWindow.Close();
				Application.Current.Shutdown();
				System.Environment.Exit(1);
			}
			SingleLaunchHelper.KeepAlive();
		}

		static void RegisterServices()
		{
			ServiceFactory.Initialize(new LayoutService(), new UserDialogService(), new ProgressService());
			ServiceFactory.Events.GetEvent<ConfigurationChangedEvent>().Subscribe(x => { InitializeKnownModules(); });
		}

		static void InitializeKnownModules()
		{
			var devicesModule = new DevicesModule.DevicesModule();
			var libraryModule = new LibraryModule.LibraryModule();
			var plansModule = new PlansModule.PlansModule();
			var securityModule = new SecurityModule.SecurityModule();
			var filtersModule = new FiltersModule.FilterModule();
			var soundsModule = new SoundsModule.SoundsModule();
			var instructionsModule = new InstructionsModule.InstructionsModule();
			var settingsModule = new SettingsModule.SettingsModule();
			if (ServiceFactory.AppSettings.ShowGC)
			{
				var groupControllerViewModel = new GroupControllerModule.GroupControllerModule();
			}
			if (ServiceFactory.AppSettings.ShowVideo)
			{
				var videoViewModel = new VideoModule.VideoModule();
				VideoService.Initialize(ServiceFactory.AppSettings.LibVlcDllsPath);
			}
			var skudModule = new SkudModule.SkudModule();
			ServiceFactory.SaveService.Reset();
		}

		static void InitializeAppSettings()
		{
			var appSettings = new AppSettings();
			appSettings.ServiceAddress = ConfigurationManager.AppSettings["ServiceAddress"] as string;
			appSettings.DefaultLogin = ConfigurationManager.AppSettings["DefaultLogin"] as string;
			appSettings.DefaultPassword = ConfigurationManager.AppSettings["DefaultPassword"] as string;
			appSettings.AutoConnect = Convert.ToBoolean(ConfigurationManager.AppSettings["AutoConnect"] as string);
			appSettings.ShowVideo = Convert.ToBoolean(ConfigurationManager.AppSettings["ShowVideo"] as string);
			appSettings.LibVlcDllsPath = ConfigurationManager.AppSettings["LibVlcDllsPath"] as string;
			appSettings.ShowGC = Convert.ToBoolean(ConfigurationManager.AppSettings["ShowGC"] as string);
			appSettings.IsDebug = Convert.ToBoolean(ConfigurationManager.AppSettings["IsDebug"] as string);
			ServiceFactory.AppSettings = appSettings;
		}
	}
}