﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace FiresecClient.Models
{
    [DataContract]
    public class Driver
    {
        [DataMember]
        public List<DriverProperty> Properties { get; set; }

        [DataMember]
        public List<Parameter> Parameters { get; set; }

        [DataMember]
        public List<InnerState> States { get; set; }

        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ShortName { get; set; }

        [DataMember]
        public bool HasAddress { get; set; }

        [DataMember]
        public bool CanEditAddress { get; set; }

        [DataMember]
        public string AddressMask { get; set; }

        [DataMember]
        public string ChildAddressMask { get; set; }

        [DataMember]
        public int ShleifCount { get; set; }

        [DataMember]
        public bool IsDeviceOnShleif { get; set; }

        [DataMember]
        public bool HasShleif { get; set; }

        [DataMember]
        public bool UseParentAddressSystem { get; set; }

        [DataMember]
        public bool IsChildAddressReservedRange { get; set; }

        [DataMember]
        public int ChildAddressReserveRangeCount { get; set; }

        [DataMember]
        public bool DisableAutoCreateChildren { get; set; }

        [DataMember]
        public List<string> Children { get; set; }

        [DataMember]
        public List<string> AvaliableChildren { get; set; }

        [DataMember]
        public bool CanAddChildren { get; set; }

        [DataMember]
        public List<string> AutoCreateChildren { get; set; }

        [DataMember]
        public bool IsRangeEnabled { get; set; }

        [DataMember]
        public int MinAddress { get; set; }

        [DataMember]
        public int MaxAddress { get; set; }

        [DataMember]
        public bool IsAutoCreate { get; set; }

        [DataMember]
        public int MinAutoCreateAddress { get; set; }

        [DataMember]
        public int MaxAutoCreateAddress { get; set; }

        [DataMember]
        public bool HasAddressMask { get; set; }

        [DataMember]
        public string ImageSource { get; set; }

        [DataMember]
        public bool HasImage { get; set; }

        [DataMember]
        public bool IsZoneDevice { get; set; }

        [DataMember]
        public bool IsZoneLogicDevice { get; set; }

        [DataMember]
        public bool CanDisable { get; set; }

        [DataMember]
        public bool IsPlaceable { get; set; }

        [DataMember]
        public bool IsIndicatorDevice { get; set; }

        [DataMember]
        public bool CanControl { get; set; }

        [DataMember]
        public bool IsBUtton { get; set; }

        [DataMember]
        public bool IsOutDevice { get; set; }

        [DataMember]
        public DeviceCategory Category { get; set; }

        [DataMember]
        public string CategoryName { get; set; }

        [DataMember]
        public DeviceType DeviceType { get; set; }

        [DataMember]
        public string DeviceTypeName { get; set; }

        [DataMember]
        public bool IsIgnore { get; set; }

        [DataMember]
        public bool IsAssadIgnore { get; set; }

        [DataMember]
        public string DriverName { get; set; }
    }
}