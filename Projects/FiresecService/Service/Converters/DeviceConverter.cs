﻿using System.Collections.Generic;
using System.Linq;
using FiresecClient.Models;

namespace FiresecClient.Converters
{
    public static class DeviceConverter
    {
        static Firesec.CoreConfig.config _firesecConfig;

        public static void Convert(Firesec.CoreConfig.config firesecConfig)
        {
            _firesecConfig = firesecConfig;

            FiresecManager.Configuration.Devices = new List<Device>();
            FiresecManager.States.DeviceStates = new List<DeviceState>();

            var rootInnerDevice = FiresecManager.CoreConfig.dev[0];
            Device rootDevice = new Device();
            rootDevice.Parent = null;
            SetInnerDevice(rootDevice, rootInnerDevice);
            FiresecManager.Configuration.Devices.Add(rootDevice);
            FiresecManager.States.DeviceStates.Add(CreateDeviceState(rootDevice));
            AddDevice(rootInnerDevice, rootDevice);

            FiresecManager.Configuration.RootDevice = rootDevice;
        }

        static void AddDevice(Firesec.CoreConfig.devType parentInnerDevice, Device parentDevice)
        {
            parentDevice.Children = new List<Device>();
            if (parentInnerDevice.dev != null)
            {
                foreach (var innerDevice in parentInnerDevice.dev)
                {
                    Device device = new Device();
                    device.Parent = parentDevice;
                    parentDevice.Children.Add(device);
                    SetInnerDevice(device, innerDevice);
                    FiresecManager.Configuration.Devices.Add(device);
                    FiresecManager.States.DeviceStates.Add(CreateDeviceState(device));
                    AddDevice(innerDevice, device);
                }
            }
        }

        static DeviceState CreateDeviceState(Device device)
        {
            DeviceState deviceState = new DeviceState();
            deviceState.ChangeEntities = new ChangeEntities();
            deviceState.Id = device.Id;
            deviceState.PlaceInTree = device.PlaceInTree;

            deviceState.InnerStates = new List<InnerState>();
            foreach (var state in device.Driver.States)
            {
                deviceState.InnerStates.Add(state.Copy());
            }

            deviceState.Parameters = new List<Parameter>();
            foreach (var parameter in device.Driver.Parameters)
            {
                deviceState.Parameters.Add(parameter.Copy());
            }

            return deviceState;
        }

        static void SetInnerDevice(Device device, Firesec.CoreConfig.devType innerDevice)
        {
            var driverId = _firesecConfig.drv.FirstOrDefault(x => x.idx == innerDevice.drv).id;
            device.DriverId = driverId;
            device.Driver = FiresecManager.Configuration.Drivers.FirstOrDefault(x => x.Id == driverId);


            device.IntAddress = System.Convert.ToInt32(innerDevice.addr);

            if (innerDevice.param != null)
            {
                var DatabaseIdParam = innerDevice.param.FirstOrDefault(x => x.name == "DB$IDDevices");
                if (DatabaseIdParam != null)
                {
                    device.DatabaseId = DatabaseIdParam.value;
                }
                var UIDParam = innerDevice.param.FirstOrDefault(x => x.name == "INT$DEV_GUID");
                if (UIDParam != null)
                {
                    device.UID = UIDParam.value;
                }
            }

            device.Properties = new List<Property>();
            if (innerDevice.prop != null)
            {
                foreach (var innerProperty in innerDevice.prop)
                {
                    Property deviceProperty = new Property();
                    deviceProperty.Name = innerProperty.name;
                    deviceProperty.Value = innerProperty.value;
                    device.Properties.Add(deviceProperty);
                }
            }

            device.Description = innerDevice.name;

            SetPlaceInTree(device);
            SetZone(device, innerDevice);
        }

        static void SetPlaceInTree(Device device)
        {
            if (device.Parent == null)
            {
                device.PlaceInTree = "";
            }
            else
            {
                string localPlaceInTree = (device.Parent.Children.Count - 1).ToString();
                if (device.Parent.PlaceInTree == "")
                {
                    device.PlaceInTree = localPlaceInTree;
                }
                else
                {
                    device.PlaceInTree = device.Parent.PlaceInTree + @"\" + localPlaceInTree;
                }
            }
        }

        static void SetZone(Device device, Firesec.CoreConfig.devType innerDevice)
        {
            if (innerDevice.inZ != null)
            {
                string zoneIdx = innerDevice.inZ[0].idz;
                string zoneNo = _firesecConfig.zone.FirstOrDefault(x => x.idx == zoneIdx).no;
                device.ZoneNo = zoneNo;
            }
            if (innerDevice.prop != null)
            {
                var property = innerDevice.prop.FirstOrDefault(x => x.name == "ExtendedZoneLogic");
                if (property != null)
                {
                    string zoneLogicstring = property.value;
                    if (string.IsNullOrEmpty(zoneLogicstring) == false)
                    {
                        var zoneLogic = SerializerHelper.GetZoneLogic(zoneLogicstring);
                        device.ZoneLogic = ZoneLogicConverter.Convert(zoneLogic);
                    }
                }
            }
        }

        public static void ConvertBack(CurrentConfiguration currentConfiguration)
        {
            Device rootDevice = currentConfiguration.RootDevice;
            Firesec.CoreConfig.devType rootInnerDevice = DeviceToInnerDevice(rootDevice);
            AddInnerDevice(rootDevice, rootInnerDevice);

            FiresecManager.CoreConfig.dev = new Firesec.CoreConfig.devType[1];
            FiresecManager.CoreConfig.dev[0] = rootInnerDevice;
        }

        static void AddInnerDevice(Device parentDevice, Firesec.CoreConfig.devType parentInnerDevice)
        {
            List<Firesec.CoreConfig.devType> childInnerDevices = new List<Firesec.CoreConfig.devType>();

            foreach (var device in parentDevice.Children)
            {
                Firesec.CoreConfig.devType childInnerDevice = DeviceToInnerDevice(device);
                childInnerDevices.Add(childInnerDevice);
                AddInnerDevice(device, childInnerDevice);
            }
            parentInnerDevice.dev = childInnerDevices.ToArray();
        }

        static Firesec.CoreConfig.devType DeviceToInnerDevice(Device device)
        {
            Firesec.CoreConfig.devType innerDevice = new Firesec.CoreConfig.devType();
            innerDevice.drv = FiresecManager.CoreConfig.drv.FirstOrDefault(x => x.id == device.Driver.Id).idx;

            if (device.Driver.HasAddress)
            {
                innerDevice.addr = device.IntAddress.ToString();
            }
            else
            {
                innerDevice.addr = "0";
            }

            if (device.ZoneNo != null)
            {
                List<Firesec.CoreConfig.inZType> zones = new List<Firesec.CoreConfig.inZType>();
                zones.Add(new Firesec.CoreConfig.inZType() { idz = device.ZoneNo });
                innerDevice.inZ = zones.ToArray();
            }

            innerDevice.prop = AddProperties(device).ToArray();

            return innerDevice;
        }

        static List<Firesec.CoreConfig.propType> AddProperties(Device device)
        {
            List<Firesec.CoreConfig.propType> propertyList = new List<Firesec.CoreConfig.propType>();

            if (device.Driver.DriverName != "Компьютер")
            {
                if ((device.Properties != null) && (device.Properties.Count > 0))
                {
                    foreach (var deviceProperty in device.Properties)
                    {
                        if ((string.IsNullOrEmpty(deviceProperty.Name) == false) && (string.IsNullOrEmpty(deviceProperty.Value)) == false)
                        {
                            //if ((device.Driver.propInfo != null) && (device.Driver.propInfo.Any(x => x.name == deviceProperty.Name)))
                            {
                                Firesec.CoreConfig.propType property = new Firesec.CoreConfig.propType();
                                property.name = deviceProperty.Name;
                                property.value = deviceProperty.Value;
                                propertyList.Add(property);
                            }
                        }
                    }
                }
            }

            if (device.ZoneLogic.Clauses.Count > 0)
            {
                var innerZoneLogic = ZoneLogicConverter.ConvertBack(device.ZoneLogic);
                //device.XZoneLogic = ZoneLogicConverter.Convert(zoneLogic);
                string zoneLogic = SerializerHelper.SetZoneLogic(innerZoneLogic);
                Firesec.CoreConfig.propType property = new Firesec.CoreConfig.propType();
                property.name = "ExtendedZoneLogic";
                property.value = zoneLogic;
                propertyList.Add(property);
            }

            return propertyList;
        }
    }
}