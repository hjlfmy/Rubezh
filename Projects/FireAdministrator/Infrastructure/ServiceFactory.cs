﻿using FiresecAPI.Models;
using Infrastructure.Client.Login;
using Infrastructure.Common;
using Microsoft.Practices.Prism.Events;
using System;
using Infrastructure.Common.Windows.ViewModels;
using Infrastructure.Services;

namespace Infrastructure
{
	public class ServiceFactory : ServiceFactoryBase
	{
		public static void Initialize(ILayoutService ILayoutService, IProgressService IProgressService, IValidationService IValidationService)
		{
			SaveService = new SaveService();
			Events = new EventAggregator();
			ResourceService = new ResourceService();
			Layout = ILayoutService;
			ProgressService = IProgressService;
			ValidationService = IValidationService;
			LoginService = new LoginService(ClientType.Administrator, "Администратор. Авторизация");
		}

		public static AppSettings AppSettings { get; set; }
		public static SaveService SaveService { get; private set; }
		public static ILayoutService Layout { get; private set; }
		public static IProgressService ProgressService { get; private set; }
		public static IValidationService ValidationService { get; private set; }
		public static LoginService LoginService { get; private set; }
		public static MenuService MenuService { get; set; }
	}
}