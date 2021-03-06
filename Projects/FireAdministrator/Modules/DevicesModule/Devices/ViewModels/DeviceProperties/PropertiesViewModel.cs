﻿using System.Collections.Generic;
using FiresecAPI.Models;
using Infrastructure;
using Infrastructure.Common.Windows.ViewModels;

namespace DevicesModule.DeviceProperties
{
	public class PropertiesViewModel : BaseViewModel
	{
		public Device Device { get; private set; }
		public List<StringPropertyViewModel> StringProperties { get; set; }
		public List<BoolPropertyViewModel> BoolProperties { get; set; }
		public List<EnumPropertyViewModel> EnumProperties { get; set; }

		public PropertiesViewModel(Device device)
		{
			Device = device;
			StringProperties = new List<StringPropertyViewModel>();
			BoolProperties = new List<BoolPropertyViewModel>();
			EnumProperties = new List<EnumPropertyViewModel>();

			foreach (var driverProperty in device.Driver.Properties)
			{
				if (driverProperty.IsHidden)
					continue;

				switch (driverProperty.DriverPropertyType)
				{
					case DriverPropertyTypeEnum.EnumType:
						EnumProperties.Add(new EnumPropertyViewModel(driverProperty, device));
						break;

					case DriverPropertyTypeEnum.StringType:
					case DriverPropertyTypeEnum.IntType:
					case DriverPropertyTypeEnum.ByteType:
						StringProperties.Add(new StringPropertyViewModel(driverProperty, device));
						break;

					case DriverPropertyTypeEnum.BoolType:
						BoolProperties.Add(new BoolPropertyViewModel(driverProperty, device));
						break;
				}
			}
		}

		public bool IsMonitoringDisabled
		{
			get { return Device.IsMonitoringDisabled; }
			set
			{
				Device.IsMonitoringDisabled = value;
				OnPropertyChanged("IsMonitoringDisabled");
				ServiceFactory.SaveService.DevicesChanged = true;
			}
		}
	}
}