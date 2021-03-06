﻿using FiresecAPI.Models;
using Infrastructure.Common.Windows.ViewModels;

namespace FiresecService.ViewModels
{
	public class DeviceStateViewModel : BaseViewModel
	{
		public DeviceStateViewModel(DriverState driverState)
		{
			DriverState = driverState;
		}

		public DriverState DriverState { get; private set; }

		public bool _isActive;
		public bool IsActive
		{
			get { return _isActive; }
			set
			{
				if (_isActive != value)
				{
					_isActive = value;
					OnPropertyChanged("IsActive");
					ImitatorViewModel.Current.Update();
				}
			}
		}
	}
}