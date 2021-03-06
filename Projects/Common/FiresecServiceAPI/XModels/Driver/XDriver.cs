﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace XFiresecAPI
{
	[DataContract]
	public class XDriver
	{
		public static Guid System_UID = new Guid("938947C5-4624-4A1A-939C-60AEEBF7B65C");

		public XDriver()
		{
			Children = new List<XDriverType>();
			Properties = new List<XDriverProperty>();
			AutoCreateChildren = new List<XDriverType>();
			AvailableStates = new List<XStateType>();
			CanEditAddress = true;
			HasAddress = true;
			IsDeviceOnShleif = true;
		}

		[DataMember]
		public XDriverType DriverType { get; set; }
		[DataMember]
		public ushort DriverTypeNo { get; set; }
		[DataMember]
		public Guid UID { get; set; }
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public string ShortName { get; set; }

		[DataMember]
		public List<XDriverProperty> Properties { get; set; }
		[DataMember]
		public List<XStateType> AvailableStates { get; set; }

		[DataMember]
		public List<XDriverType> Children { get; set; }
		[DataMember]
		public List<XDriverType> AutoCreateChildren { get; set; }

		[DataMember]
		public bool HasAddress { get; set; }
		[DataMember]
		public bool CanEditAddress { get; set; }
		[DataMember]
		public bool IsRangeEnabled { get; set; }
		[DataMember]
		public bool IsAutoCreate { get; set; }
		[DataMember]
		public byte MinAddress { get; set; }
		[DataMember]
		public byte MaxAddress { get; set; }
		[DataMember]
		public int MaxAddressOnShleif { get; set; }
		[DataMember]
		public bool IsDeviceOnShleif { get; set; }

		[DataMember]
		public bool HasLogic { get; set; }
		[DataMember]
		public bool HasZone { get; set; }
		[DataMember]
		public bool IsControlDevice { get; set; }

		[DataMember]
		public bool IsGroupDevice { get; set; }
		[DataMember]
		public XDriverType GroupDeviceChildType { get; set; }
		[DataMember]
		public byte GroupDeviceChildrenCount { get; set; }

		public string ImageSource
		{
			get { return "/Controls;component/GKIcons/" + this.DriverType.ToString() + ".png"; }
		}
	}
}