﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Common;
using CurrentDeviceModule.Views;
using DeviceControls;
using FiresecClient;
using System.Windows;
using DevicesModule.ViewModels;
using DevicesModule.Views;
using FiresecAPI.Models;

namespace CurrentDeviceModule.ViewModels
{
    public class CurrentDeviceViewModel : BaseViewModel
    {
        public CurrentDeviceViewModel()
        {
            FiresecEventSubscriber.DeviceStateChangedEvent += new Action<string>(OnDeviceStateChanged);
            CurrentDeviceControl = new DeviceControl();
            CurrentDevice = new Device();
            IsCurrentDeviceSelected = false;
            ShowPropertiesCommand = new RelayCommand(OnShowProperties);
        }

        public void Inicialize(string deviceId)
        {
            DeviceId = deviceId;
            CurrentDevice = FiresecManager.DeviceConfiguration.Devices.FirstOrDefault(x => x.Id == DeviceId);
            CurrentDeviceState = FiresecManager.DeviceStates.DeviceStates.FirstOrDefault(x => x.Id == DeviceId);
            CurrentDeviceControl.DriverId = DriverId;
            Update();
            IsCurrentDeviceSelected = true;
        }

        DeviceControl _currentDeviceControl;
        public DeviceControl CurrentDeviceControl
        {
            get { return _currentDeviceControl; }
            set
            {
                _currentDeviceControl = value;
            }
        }

        public Device CurrentDevice { get; private set; }
        public DeviceState CurrentDeviceState { get; private set; }
        public string DeviceId { get; set; }

        public StateType StateType
        {
            get { return CurrentDeviceState.StateType; }
        }

        public string DriverId
        {
            get { return CurrentDevice.Driver.Id; }
        }

        string _toolTip;
        public string ToolTip
        {
            get { return _toolTip; }
            set
            {
                _toolTip = value;
                OnPropertyChanged("ToolTip");
            }
        }

        bool _isCurrentDeviceSelected;
        public bool IsCurrentDeviceSelected
        {
            get { return _isCurrentDeviceSelected; }
            set { _isCurrentDeviceSelected = value; }
        }

        public void SelectDevice()
        {
            DeviceTreeViewModel newDeviceTreeViewModel = new DeviceTreeViewModel();
            newDeviceTreeViewModel.Initialize();
            DeviceTreeView newDeviceTreeView = new DeviceTreeView();
            newDeviceTreeView.DataContext = newDeviceTreeViewModel;
            newDeviceTreeViewModel.Closing += newDeviceTreeView.Close;
            newDeviceTreeView.ShowDialog();

            if (!string.IsNullOrWhiteSpace(newDeviceTreeViewModel.DeviceId))
            {
                DeviceId = string.Copy(newDeviceTreeViewModel.DeviceId);
                Inicialize(DeviceId);
            }
        }

        public void UpdateToolTip()
        {
            //string str = "";
            //str = CurrentDevice.Address + " - " + CurrentDevice.Driver.ShortName + "\n";

            //if (CurrentDeviceState.ParentStringStates != null)
            //    foreach (var parentState in CurrentDeviceState.ParentStringStates)
            //    {
            //        str += parentState + "\n";
            //    }

            //if (deviceState.SelfStates != null)
            //    foreach (var selfState in deviceState.SelfStates)
            //    {
            //        str += selfState + "\n";
            //    }

            //if (CurrentDeviceState.Parameters != null)
            //    foreach (var parameter in CurrentDeviceState.Parameters)
            //    {
            //        if (parameter.Visible)
            //        {
            //            if (string.IsNullOrEmpty(parameter.Value))
            //                continue;
            //            if (parameter.Value == "<NULL>")
            //                continue;
            //            str += parameter.Caption + " - " + parameter.Value + "\n";
            //        }
            //    }
            //ToolTip = str;
        }

        public void UpdateContextMenu()
        {

        }
        
        void Update()
        {
            UpdateToolTip();
            CurrentDeviceControl.StateType = StateType;
        }

        void OnDeviceStateChanged(string id)
        {
            if (DeviceId == id) ;
            {
                Update();
            }
        }

        public RelayCommand ShowPropertiesCommand { get; private set; }
        public void OnShowProperties()
        {
            DeviceDetailsViewModel deviceDetailsViewModel = new DeviceDetailsViewModel(DeviceId);
            CurrentDeviceDetailsView deviceDetailsView = new CurrentDeviceDetailsView();
            deviceDetailsView.DataContext = deviceDetailsViewModel;
            deviceDetailsView.ShowDialog();
        }
    }
}