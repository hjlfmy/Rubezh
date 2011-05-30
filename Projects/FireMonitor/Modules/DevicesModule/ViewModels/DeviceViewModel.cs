﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure;
using System.Collections.ObjectModel;
using FiresecClient;
using DevicesModule.Views;
using System.Diagnostics;
using Infrastructure.Common;
using Infrastructure.Events;
using Firesec;

namespace DevicesModule.ViewModels
{
    public class DeviceViewModel : TreeBaseViewModel<DeviceViewModel>
    {
        Device _device;
        Firesec.Metadata.drvType _driver;

        public DeviceViewModel()
        {
            ShowPlanCommand = new RelayCommand(OnShowPlan);
            ShowZoneCommand = new RelayCommand(OnShowZone);
            DisableCommand = new RelayCommand(OnDisable);
            ShowPropertiesCommand = new RelayCommand(OnShowProperties);
        }

        public void Initialize(Device device, ObservableCollection<DeviceViewModel> sourceDevices)
        {
            Source = sourceDevices;

            this._device = device;
            _driver = FiresecManager.CurrentConfiguration.Metadata.drv.FirstOrDefault(x => x.id == device.DriverId);

            UpdateParameters();
        }

        public void UpdateParameters()
        {
            DeviceState deviceState = FiresecManager.CurrentStates.DeviceStates.FirstOrDefault(x => x.Id == _device.Id);

            Update();

            if (deviceState.Parameters != null)
            {
                foreach (Parameter parameter in deviceState.Parameters)
                {
                    switch (parameter.Name)
                    {
                        case "FailureType":
                            FailureType = parameter.Value;
                            break;

                        case "AlarmReason":
                            AlarmReason = parameter.Value;
                            break;

                        case "Smokiness":
                            Smokiness = parameter.Value;
                            break;

                        case "Dustiness":
                            Dustiness = parameter.Value;
                            break;

                        case "Temperature":
                            Temperature = parameter.Value;
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        public string Id
        {
            get { return _device.Id; }
        }

        public string DriverId
        {
            get
            {
                if (_device != null)
                    return _device.DriverId;
                return null;
            }
        }

        public bool IsZoneDevice
        {
            get
            {
                if ((_driver.minZoneCardinality == "0") && (_driver.maxZoneCardinality == "0"))
                {
                    return false;
                }
                return true;
            }
        }

        public bool IsZoneLogicDevice
        {
            get
            {
                if ((_driver.options != null) && (_driver.options.Contains("ExtendedZoneLogic")))
                {
                    return true;
                }
                return false;
            }
        }

        public string PresentationZone
        {
            get
            {
                if (IsZoneDevice)
                {
                    if (string.IsNullOrEmpty(_device.ZoneNo))
                        return "";

                    Zone zone = FiresecManager.CurrentConfiguration.Zones.FirstOrDefault(x => x.No == _device.ZoneNo);
                    return _device.ZoneNo + "." + zone.Name;
                }
                if (IsZoneLogicDevice)
                {
                    return ZoneLogicToText.Convert(_device.ZoneLogic);
                }
                return "";
            }
        }

        public string ShortDriverName
        {
            get
            {
                return _driver.shortName;
            }
        }

        public string DriverName
        {
            get
            {
                return _driver.name;
            }
        }

        public bool HasAddress
        {
            get
            {
                return (!string.IsNullOrEmpty(Address));
            }
        }

        public string Address
        {
            get
            {
                return _device.Address;
            }
        }

        public string PresentationAddress
        {
            get
            {
                if (_device.Address == "0")
                    return "";
                return _device.Address;
            }
        }

        public string Description
        {
            get
            {
                return _device.Description;
            }
        }

        public string ConnectedTo
        {
            get
            {
                if (Parent == null)
                    return null;
                else
                {
                    string parentPart = Parent.ShortDriverName;
                    if (Parent._driver.ar_no_addr != "1")
                        parentPart += " - " + Parent.Address;

                    if (Parent.ConnectedTo == null)
                        return parentPart;

                    if (Parent.Parent.ConnectedTo == null)
                        return parentPart;

                    return parentPart + @"\" + Parent.ConnectedTo;
                }
            }
        }

        public bool HasImage
        {
            get
            {
                return (ImageSource != @"C:/Program Files/Firesec/Icons/Device_Device.ico");
            }
        }

        public string ImageSource
        {
            get
            {
                string ImageName;
                if (!string.IsNullOrEmpty(_driver.dev_icon))
                {
                    ImageName = _driver.dev_icon;
                }
                else
                {
                    Firesec.Metadata.classType metadataClass = FiresecManager.CurrentConfiguration.Metadata.@class.FirstOrDefault(x => x.clsid == _driver.clsid);
                    ImageName = metadataClass.param.FirstOrDefault(x => x.name == "Icon").value;
                }

                return @"C:/Program Files/Firesec/Icons/" + ImageName + ".ico";
                //return @"pack://application:,,,/Icons/" + ImageName + ".ico";
            }
        }

        public bool CanIgnore
        {
            get
            {
                if (_driver.options != null)
                {
                    if (_driver.options.Contains("Ignorable"))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public ObservableCollection<string> SelfStates
        {
            get
            {
                ObservableCollection<string> selfStates = new ObservableCollection<string>();
                DeviceState deviceState = FiresecManager.CurrentStates.DeviceStates.FirstOrDefault(x => x.Id == _device.Id);
                if (deviceState.SelfStates != null)
                    foreach (string selfState in deviceState.SelfStates)
                    {
                        selfStates.Add(selfState);
                    }
                return selfStates;
            }
        }

        public ObservableCollection<string> ParentStates
        {
            get
            {
                ObservableCollection<string> parentStates = new ObservableCollection<string>();
                DeviceState deviceState = FiresecManager.CurrentStates.DeviceStates.FirstOrDefault(x => x.Id == _device.Id);
                if (deviceState.ParentStringStates != null)
                    foreach (string parentState in deviceState.ParentStringStates)
                    {
                        parentStates.Add(parentState);
                    }
                return parentStates;
            }
        }

        public ObservableCollection<string> Parameters
        {
            get
            {
                ObservableCollection<string> parameters = new ObservableCollection<string>();
                DeviceState deviceState = FiresecManager.CurrentStates.DeviceStates.FirstOrDefault(x => x.Id == _device.Id);
                if (deviceState.Parameters != null)
                    foreach (Parameter parameter in deviceState.Parameters)
                    {
                        if (parameter.Visible)
                        {
                            if (string.IsNullOrEmpty(parameter.Value))
                                continue;
                            if (parameter.Value == "<NULL>")
                                continue;
                            parameters.Add(parameter.Caption + " - " + parameter.Value);
                        }
                    }
                return parameters;
            }
        }

        public State State
        {
            get
            {
                DeviceState deviceState = FiresecManager.CurrentStates.DeviceStates.FirstOrDefault(x => x.Id == _device.Id);
                return deviceState.State;
            }
        }

        string _failureType;
        public string FailureType
        {
            get { return _failureType; }
            set
            {
                if ((string.IsNullOrEmpty(value)) || (value == "<NULL>"))
                    _failureType = " - ";
                else
                    _failureType = value;
                OnPropertyChanged("FailureType");
            }
        }

        string _alarmReason;
        public string AlarmReason
        {
            get { return _alarmReason; }
            set
            {
                if ((string.IsNullOrEmpty(value)) || (value == "<NULL>"))
                    _alarmReason = " - ";
                else
                    _alarmReason = value;
                OnPropertyChanged("AlarmReason");
            }
        }

        string _smokiness;
        public string Smokiness
        {
            get { return _smokiness; }
            set
            {
                if ((string.IsNullOrEmpty(value)) || (value == "<NULL>"))
                    _smokiness = " - ";
                else
                    _smokiness = value;
                OnPropertyChanged("Smokiness");
            }
        }

        string _dustiness;
        public string Dustiness
        {
            get { return _dustiness; }
            set
            {
                if ((string.IsNullOrEmpty(value)) || (value == "<NULL>"))
                    _dustiness = " - ";
                else
                    _dustiness = value;
                OnPropertyChanged("Dustiness");
            }
        }

        string _temperature;
        public string Temperature
        {
            get { return _temperature; }
            set
            {
                if ((string.IsNullOrEmpty(value)) || (value == "<NULL>"))
                    _temperature = " - ";
                else
                    _temperature = value;
                OnPropertyChanged("Temperature");
            }
        }

        public void Update()
        {
            OnPropertyChanged("State");
        }

        public RelayCommand ShowPlanCommand { get; private set; }
        void OnShowPlan()
        {
            ServiceFactory.Events.GetEvent<ShowDeviceOnPlanEvent>().Publish(_device.Id);
        }

        public RelayCommand ShowZoneCommand { get; private set; }
        void OnShowZone()
        {
            string zoneNo = _device.ZoneNo;
            if (string.IsNullOrEmpty(zoneNo) == false)
            {
                ServiceFactory.Events.GetEvent<ShowZoneEvent>().Publish(zoneNo);
            }
        }

        public RelayCommand DisableCommand { get; private set; }
        void OnDisable()
        {
            ;
        }

        public RelayCommand ShowPropertiesCommand { get; private set; }
        void OnShowProperties()
        {
            ServiceFactory.Events.GetEvent<ShowDeviceDetailsEvent>().Publish(_device.Id);
        }
    }
}
