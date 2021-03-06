﻿using System.Collections.Generic;
using FiresecAPI.Models;
using Infrastructure;
using Infrastructure.Common;
using Infrastructure.Common.Navigation;
using Infrastructure.Events;
using SkudModule.ViewModels;
using Infrastructure.Client;

namespace SkudModule
{
	public class SkudModule : ModuleBase
	{
		private SkudViewModel _skudViewModel;
		private EmployeeCardIndexViewModel _employeeCardIndexViewModel;
		private EmployeeDepartmentsViewModel _employeeDepartmentsViewModel;
		private EmployeePositionsViewModel _employeePositionsViewModel;
		private EmployeeGroupsViewModel _employeeGroupsViewModel;

		public SkudModule()
		{
			ServiceFactory.Events.GetEvent<ShowSkudEvent>().Unsubscribe(OnShowSkud);
			ServiceFactory.Events.GetEvent<ShowSkudEvent>().Subscribe(OnShowSkud);
		}

		private void OnShowSkud(object obj)
		{
			//ServiceFactory.Layout.Show(_skudViewModel);
		}

		public override void Initialize()
		{
			_skudViewModel = new SkudViewModel();
			_employeeCardIndexViewModel = new EmployeeCardIndexViewModel();
			_employeeCardIndexViewModel.Initialize();

			_employeeDepartmentsViewModel = new EmployeeDepartmentsViewModel();
			_employeeGroupsViewModel = new EmployeeGroupsViewModel();
			_employeePositionsViewModel = new EmployeePositionsViewModel();
		}
		public override IEnumerable<NavigationItem> CreateNavigation()
		{
			return new List<NavigationItem>()
			{
				new NavigationItem("СКУД", null, new List<NavigationItem>()
				{
					new NavigationItem<ShowEmployeeCardIndexEvent>(_employeeCardIndexViewModel, "Картотека",null),
					new NavigationItem<ShowPassCardEvent>(_skudViewModel, "Пропуск",null),
					new NavigationItem("Справочники",null, new List<NavigationItem>()
					{
						new NavigationItem<ShowEmployeePositionsEvent>(_employeePositionsViewModel, "Должности",null),
						new NavigationItem<ShowEmployeeDepartmentsEvent>(_employeeDepartmentsViewModel, "Подразделения",null),
						new NavigationItem<ShowEmployeeGroupsEvent>(_employeeGroupsViewModel, "Группы",null),
					})
				}, PermissionType.Adm_SKUD),
			};
		}
		public override string Name
		{
			get { return "СКУД"; }
		}
	}
}