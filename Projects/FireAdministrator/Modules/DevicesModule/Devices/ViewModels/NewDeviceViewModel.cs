﻿using System.Collections.Generic;
using System.Linq;
using FiresecAPI.Models;
using FiresecClient;
using Infrastructure.Common;

namespace DevicesModule.ViewModels
{
    public class NewDeviceViewModel : DialogContent
    {
        public NewDeviceViewModel(DeviceViewModel parent)
        {
            //ViewModels.Devices.DriversView driversView = new ViewModels.Devices.DriversView();
            //driversView.ShowDialog();

            Title = "Новое устройство";
            AddCommand = new RelayCommand(OnAdd, CanAdd);
            CancelCommand = new RelayCommand(OnCancel);
            _parentDeviceViewModel = parent;
            _parent = _parentDeviceViewModel.Device;
        }

        DeviceViewModel _parentDeviceViewModel;
        Device _parent;

        public IEnumerable<Driver> Drivers
        {
            get
            {
                return from Driver driver in FiresecManager.Drivers
                       where _parent.Driver.AvaliableChildren.Contains(driver.Id)
                       select driver;
            }
        }

        Driver _selectedDriver;
        public Driver SelectedDriver
        {
            get { return _selectedDriver; }
            set
            {
                _selectedDriver = value;
                OnPropertyChanged("SelectedDriver");
            }
        }

        Device ParentAddressSystemDevice
        {
            get
            {
                Device parentAddressSystemDevice = _parent;
                while (parentAddressSystemDevice.Driver.UseParentAddressSystem)
                {
                    parentAddressSystemDevice = parentAddressSystemDevice.Parent;
                }
                return parentAddressSystemDevice;
            }
        }

        void AddChildAddressSystemDevice(List<Device> childAddressSystemDevices, Device parentDevice)
        {
            foreach (var childDevice in parentDevice.Children)
            {
                if (childDevice.Driver.UseParentAddressSystem)
                {
                    childAddressSystemDevices.Add(childDevice);
                    AddChildAddressSystemDevice(childAddressSystemDevices, childDevice);
                }
            }
        }

        List<Device> ChildAddressSystemDevices
        {
            get
            {
                var childAddressSystemDevices = new List<Device>();
                AddChildAddressSystemDevice(childAddressSystemDevices, ParentAddressSystemDevice);
                return childAddressSystemDevices;
            }
        }

        List<int> AvaliableAddresses
        {
            get { return NewDeviceHelper.GetAvaliableAddresses(SelectedDriver, ParentAddressSystemDevice); }
        }

        int GetNewAddress()
        {
            var avaliableAddresses = NewDeviceHelper.GetAvaliableAddresses(SelectedDriver, ParentAddressSystemDevice);

            int maxIndex = -1;
            for (int i = 0; i < avaliableAddresses.Count; ++i)
            {
                if (ParentAddressSystemDevice.Children.Any(x => x.IntAddress == avaliableAddresses[i]))
                    maxIndex = i;

                if (ChildAddressSystemDevices.Any(x => x.IntAddress == avaliableAddresses[i]))
                    maxIndex = i;
            }

            if (maxIndex == -1)
                return avaliableAddresses[0];

            int address = avaliableAddresses[maxIndex];
            if (avaliableAddresses.Count() > maxIndex + 1)
            {
                address = avaliableAddresses[maxIndex + 1];
            }

            return address;
        }

        void AddDevice(Device device, DeviceViewModel parentDeviceViewModel)
        {
            var deviceViewModel = new DeviceViewModel();
            deviceViewModel.Initialize(device, _parentDeviceViewModel.Source);
            deviceViewModel.Parent = parentDeviceViewModel;
            parentDeviceViewModel.Children.Add(deviceViewModel);

            foreach (var childDevice in device.Children)
            {
                AddDevice(childDevice, deviceViewModel);
            }
        }

        public bool CanAdd(object obj)
        {
            return (SelectedDriver != null);
        }

        public RelayCommand AddCommand { get; private set; }
        void OnAdd()
        {
            if (CanAdd(null))
            {
                int address = GetNewAddress();
                Device device = _parent.AddChild(SelectedDriver, address);
                AddDevice(device, _parentDeviceViewModel);
            }

            _parentDeviceViewModel.Update();
            FiresecManager.DeviceConfiguration.Update();
            Close(true);
        }

        public RelayCommand CancelCommand { get; private set; }
        void OnCancel()
        {
            Close(false);
        }
    }
}