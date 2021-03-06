﻿using System;
using System.Collections.Generic;
using FiresecClient;
using Infrastructure;
using Infrastructure.Common;
using Infrastructure.Common.Navigation;
using Infrastructure.Events;
using PlansModule.ViewModels;
using Infrastructure.Client;

namespace PlansModule
{
	public class PlansModuleLoader : ModuleBase
	{
		private PlansViewModel PlansViewModel;
		private NavigationItem _planNavigationItem;

		public PlansModuleLoader()
		{
			ServiceFactory.Events.GetEvent<ShowDeviceOnPlanEvent>().Subscribe(OnShowDeviceOnPlan);
			ServiceFactory.Events.GetEvent<ShowZoneOnPlanEvent>().Subscribe(OnShowZoneOnPlan);
			PlansViewModel = new PlansViewModel();
		}

		void OnShowDeviceOnPlan(Guid deviceUID)
		{
			var hasDeviceOnPlan = PlansViewModel.ShowDevice(deviceUID);
			if (hasDeviceOnPlan)
				ServiceFactory.Events.GetEvent<ShowPlansEvent>().Publish(null);
		}
		void OnShowZoneOnPlan(int zoneNo)
		{
			ServiceFactory.Events.GetEvent<ShowPlansEvent>().Publish(null);
			PlansViewModel.ShowZone(zoneNo);
		}

		public override void Initialize()
		{
			_planNavigationItem.IsVisible = FiresecManager.PlansConfiguration.Plans.Count > 0;
			PlansViewModel.Initialize();
		}
		public override IEnumerable<NavigationItem> CreateNavigation()
		{
			_planNavigationItem = new NavigationItem<ShowPlansEvent>(PlansViewModel, "Планы", "/Controls;component/Images/map.png");
			return new List<NavigationItem>() { _planNavigationItem };
		}

		public override string Name
		{
			get { return "Графические планы"; }
		}
	}
}