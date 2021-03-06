﻿using System.Collections.ObjectModel;
using FiresecAPI.Models;
using Infrastructure;
using Infrastructure.Common;
using Infrustructure.Plans.Events;
using XFiresecAPI;

namespace GKModule.Plans.ViewModels
{
	public class XDeviceViewModel : TreeBaseViewModel<XDeviceViewModel>
	{
		public XDevice Device { get; private set; }

		public XDeviceViewModel(XDevice device, ObservableCollection<XDeviceViewModel> sourceDevices)
		{
			ShowOnPlanCommand = new RelayCommand(OnShowOnPlan);
			Source = sourceDevices;
			Device = device;
		}

		public bool IsOnPlan
		{
			get { return Device.PlanElementUIDs.Count > 0; }
		}

		public void Update()
		{
			OnPropertyChanged("IsOnPlan");
		}

		public RelayCommand ShowOnPlanCommand { get; private set; }
		void OnShowOnPlan()
		{
			if (Device.PlanElementUIDs.Count > 0)
				ServiceFactory.Events.GetEvent<FindElementEvent>().Publish(Device.PlanElementUIDs[0]);
		}
	}
}