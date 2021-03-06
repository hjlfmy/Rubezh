﻿using FiresecAPI.Models;
using Infrastructure.Common.Windows.ViewModels;

namespace JournalModule.ViewModels
{
	public class DeviceViewModel : BaseViewModel
	{
		public Device Device { get; private set; }

		public DeviceViewModel(Device device)
		{
			Device = device;
			IsChecked = false;
		}

		bool _isChecked;
		public bool IsChecked
		{
			get { return _isChecked; }
			set
			{
				_isChecked = value;
				OnPropertyChanged("IsChecked");
			}
		}

		public string DatabaseName
		{
			get
			{
				var name = Device.Driver.ShortName + " " + Device.DottedAddress + " ";
				if (string.IsNullOrEmpty(Device.Description) == false)
				{
					name += " (" + Device.Description + ")";
				}
				return name;
			}
		}
	}
}