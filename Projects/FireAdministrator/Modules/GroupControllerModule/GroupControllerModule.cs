﻿using System.Collections.Generic;
using Common.GK;
using FiresecAPI.Models;
using FiresecClient;
using GKModule.Plans;
using GKModule.Validation;
using GKModule.ViewModels;
using Infrastructure;
using Infrastructure.Common;
using Infrastructure.Common.Navigation;
using Infrastructure.Common.Validation;
using Infrastructure.Events;
using Infrustructure.Plans.Events;
using System;
using System.Linq;
using Infrastructure.Client;

namespace GKModule
{
	public class GroupControllerModule : ModuleBase, IValidationModule
	{
		private DevicesViewModel _devicesViewModel;
		private ZonesViewModel _zonesViewModel;
		private DirectionsViewModel _directionsViewModel;
        private FiltersViewModel _filtersViewModel;

		public GroupControllerModule()
		{
			_devicesViewModel = new DevicesViewModel();
			_zonesViewModel = new ZonesViewModel();
			_directionsViewModel = new DirectionsViewModel();
            _filtersViewModel = new FiltersViewModel();
		}

		public override void Initialize()
		{
			_devicesViewModel.Initialize();
			_zonesViewModel.Initialize();
			_directionsViewModel.Initialize();
            _filtersViewModel.Initialize();

			ServiceFactory.Events.GetEvent<RegisterPlanExtensionEvent<Plan>>().Publish(new GKPlanExtension());
		}
		public override IEnumerable<NavigationItem> CreateNavigation()
		{
			return new List<NavigationItem>()
			{
				new NavigationItem("ГК", null, new List<NavigationItem>()
				{
					new NavigationItem<ShowXDeviceEvent, Guid>(_devicesViewModel, "Устройства", "/Controls;component/Images/tree.png", null, null, Guid.Empty),
					new NavigationItem<ShowXZoneEvent, Guid>(_zonesViewModel, "Зоны", "/Controls;component/Images/zones.png", null, null, Guid.Empty),
					new NavigationItem<ShowXDirectionEvent, Guid>(_directionsViewModel, "Направления", "/Controls;component/Images/direction.png", null, null, Guid.Empty),
                    new NavigationItem<ShowXJournalFilterEvent, object>(_filtersViewModel, "Фильтры", "/Controls;component/Images/filter.png")
				}),
			};
		}
		public override string Name
		{
			get { return "Групповой контроллер"; }
		}
		public override void RegisterResource()
		{
			base.RegisterResource();
			var resourceService = new ResourceService();
			resourceService.AddResource(new ResourceDescription(GetType().Assembly, "Plans/DataTemplates/Dictionary.xaml"));
		}


		#region IValidationModule Members

		public IEnumerable<IValidationError> Validate()
		{
			return Validator.Validate();
		}

		#endregion
	}
}