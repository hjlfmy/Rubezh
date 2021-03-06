﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FiresecAPI.Models;
using FiresecClient;
using Infrastructure.Common;
using Infrastructure.Common.Windows.ViewModels;

namespace DevicesModule.ViewModels
{
    public class AutoSearchViewModel : SaveCancelDialogViewModel
    {
        public ObservableCollection<DeviceViewModel> DeviceViewModels { get; set; }
        List<AutoSearchDeviceViewModel> allDevices;

        public AutoSearchViewModel(DeviceConfiguration autodetectedDeviceConfiguration)
        {
            Title = "Добавление устройств";
            ContinueCommand = new RelayCommand(OnContinue);

            allDevices = new List<AutoSearchDeviceViewModel>();
            Devices = new List<AutoSearchDeviceViewModel>();
            Devices.Add(AddDevice(autodetectedDeviceConfiguration.RootDevice, null));
        }

        AutoSearchDeviceViewModel AddDevice(Device device, AutoSearchDeviceViewModel parentDeviceViewModel)
        {
            var deviceViewModel = new AutoSearchDeviceViewModel(device);

            foreach (var childDevice in device.Children)
            {
                if (childDevice.Driver == null)
                    continue;

                var childDeviceViewModel = AddDevice(childDevice, deviceViewModel);
                deviceViewModel.Children.Add(childDeviceViewModel);
            }

            allDevices.Add(deviceViewModel);
            return deviceViewModel;
        }

        public List<AutoSearchDeviceViewModel> Devices { get; set; }

        public RelayCommand ContinueCommand { get; private set; }
        void OnContinue()
        {
            Close(true);
        }

        void AddFromTree(AutoSearchDeviceViewModel parentAutoDetectedDevice)
        {
            foreach (var autodetectedDevice in parentAutoDetectedDevice.Children)
            {
                if (autodetectedDevice.IsSelected)
                {
                    AddAutoDevice(autodetectedDevice);
                    AddFromTree(autodetectedDevice);
                }
            }
        }

        void AddAutoDevice(AutoSearchDeviceViewModel autoDetectedDevice)
        {
            var device = autoDetectedDevice.Device;
			var parentDevice = FiresecManager.Devices.FirstOrDefault(x => x.PathId == device.Parent.PathId);
            parentDevice.Children.Add(device);

            var parentDeviceViewModel = DeviceViewModels.FirstOrDefault(x => x.Device.UID == parentDevice.UID);

            var deviceViewModel = new DeviceViewModel(device, parentDeviceViewModel.Source);
            deviceViewModel.Parent = parentDeviceViewModel;
            parentDeviceViewModel.Children.Add(deviceViewModel);

            parentDeviceViewModel.Update();

			FiresecManager.FiresecConfiguration.DeviceConfiguration.Update();
        }

		protected override bool Save()
		{
			AddFromTree(Devices[0]);
			Close(false);
			return false;
		}
    }
}