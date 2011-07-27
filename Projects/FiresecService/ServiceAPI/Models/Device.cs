﻿using System.Collections.Generic;
using System.Linq;
using FiresecClient.Converters;
using System.Runtime.Serialization;

namespace FiresecClient.Models
{
    [DataContract]
    public class Device
    {
        public Device()
        {
            Properties = new List<Property>();
            Children = new List<Device>();
        }

        public Driver Driver { get; set; }
        public Device Parent { get; set; }

        [DataMember]
        public List<Device> Children { get; set; }

        [DataMember]
        public string DatabaseId { get; set; }

        [DataMember]
        public string DriverId { get; set; }

        [DataMember]
        public string PlaceInTree { get; set; }

        [DataMember]
        public int IntAddress { get; set; }

        [DataMember]
        public string ZoneNo { get; set; }

        //[DataMember]
        public ZoneLogic ZoneLogic { get; set; }

        [DataMember]
        public List<Property> Properties { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string UID { get; set; }

        public List<ValidationError> ValidationErrors { get; set; }

        public string AddressFullPath
        {
            get
            {
                string address = IntAddress.ToString();

                var serialNoProperty = Properties.FirstOrDefault(x => x.Name == "SerialNo");
                if (serialNoProperty != null)
                    address = serialNoProperty.Value;

                if (Driver.IsDeviceOnShleif)
                {
                    address = AddressConverter.IntToStringAddress(Driver, IntAddress);
                }

                return address;
            }
        }

        public string Address
        {
            get
            {
                return PresentationAddress;
            }
        }

        public string PresentationAddress
        {
            get
            {
                if (Driver.HasAddress == false)
                {
                    return "";
                }

                string address = AddressConverter.IntToStringAddress(Driver, IntAddress);

                if (Driver.IsChildAddressReservedRange)
                {
                    int endAddress = IntAddress + Driver.ChildAddressReserveRangeCount;
                    if (endAddress / 256 != IntAddress / 256)
                        endAddress = (IntAddress / 256) * 256 + 255;
                    address += " - " + AddressConverter.IntToStringAddress(Driver, endAddress);
                }

                return address;
            }
        }

        public void SetAddress(string address)
        {
            IntAddress = AddressConverter.StringToIntAddress(Driver, address);
        }

        public string Id
        {
            get
            {
                string currentId = Driver.Id + ":" + AddressFullPath;
                if (Parent != null)
                {
                    return Parent.Id + @"/" + currentId;
                }
                return currentId;
            }
        }

        public string DottedAddress
        {
            get
            {
                string address = "";
                foreach (var parentDevice in AllParents)
                {
                    if (parentDevice.Driver.HasAddress)
                    {
                        address += parentDevice.PresentationAddress + ".";
                    }
                }
                if (Driver.HasAddress)
                {
                    address += PresentationAddress + ".";
                }
                if (address.EndsWith("."))
                    address = address.Remove(address.Length - 1);

                return address;
            }
        }

        public List<Device> AllParents
        {
            get
            {
                if (Parent == null)
                    return new List<Device>();

                List<Device> allParents = Parent.AllParents;
                allParents.Add(Parent);
                return allParents;
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
                    string parentPart = Parent.Driver.ShortName;
                    if (Parent.Driver.HasAddress)
                        parentPart += " - " + Parent.PresentationAddress;

                    if (Parent.ConnectedTo == null)
                        return parentPart;

                    if (Parent.Parent.ConnectedTo == null)
                        return parentPart;

                    return parentPart + @"\" + Parent.ConnectedTo;
                }
            }
        }

        public string PresentationZone
        {
            get
            {
                //if (Driver.IsZoneDevice)
                //{
                //    Zone zone = FiresecManager.Configuration.Zones.FirstOrDefault(x => x.No == ZoneNo);
                //    if (zone != null)
                //    {
                //        return zone.PresentationName;
                //    }
                //    return "";
                //}
                //if (Driver.IsZoneLogicDevice)
                //{
                //    return ZoneLogic.ToString();
                //}
                return "";
            }
        }

        public Device Copy(bool fullCopy)
        {
            Device newDevice = new Device();
            newDevice.Driver = Driver;
            newDevice.IntAddress = IntAddress;
            newDevice.Description = Description;
            newDevice.ZoneNo = ZoneNo;

            if (fullCopy)
            {
                newDevice.DatabaseId = DatabaseId;
            }

            newDevice.ZoneLogic = new Models.ZoneLogic();
            newDevice.ZoneLogic.JoinOperator = ZoneLogic.JoinOperator;
            foreach (var clause in ZoneLogic.Clauses)
            {
                Clause newClause = new Clause();
                newClause.State = clause.State;
                newClause.Operation = clause.Operation;
                newClause.Zones = clause.Zones.ToList();
                newDevice.ZoneLogic.Clauses.Add(newClause);
            }

            List<Property> copyProperties = new List<Property>();
            foreach (var property in Properties)
            {
                Property copyProperty = new Property();
                copyProperty.Name = property.Name;
                copyProperty.Value = property.Value;
                copyProperties.Add(copyProperty);
            }
            newDevice.Properties = copyProperties;

            newDevice.Children = new List<Device>();
            foreach (var childDevice in Children)
            {
                Device newChildDevice = childDevice.Copy(fullCopy);
                newChildDevice.Parent = newDevice;
                newDevice.Children.Add(newChildDevice);
            }

            return newDevice;
        }

        public Device AddChild(Driver newDriver, int newAddress)
        {
            Device device = new Device();
            device.Driver = newDriver;
            device.IntAddress = newAddress;
            Children.Add(device);
            device.Parent = this;
            AddAutoCreateChildren(device);

            return device;
        }

        void AddAutoCreateChildren(Device device)
        {
            //foreach (var autoCreateDriverId in device.Driver.AutoCreateChildren)
            //{
            //    var autoCreateDriver = FiresecManager.Configuration.Drivers.FirstOrDefault(x => x.Id == autoCreateDriverId);

            //    for (int i = autoCreateDriver.MinAutoCreateAddress; i <= autoCreateDriver.MaxAutoCreateAddress; i++)
            //    {
            //        Device childDevice = new Device();
            //        childDevice.Driver = autoCreateDriver;
            //        childDevice.IntAddress = i;
            //        device.Children.Add(childDevice);

            //        AddAutoCreateChildren(childDevice);
            //    }
            //}
        }
    }
}