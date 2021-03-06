﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace XFiresecAPI
{
	[DataContract]
	public class XZone : XBinaryBase
	{
		public XZone()
		{
            UID = Guid.NewGuid();
			Fire1Count = 2;
			Fire2Count = 3;
			DeviceUIDs = new List<Guid>();
		}

		public List<XDevice> Devices { get; set; }

		[DataMember]
		public Guid UID { get; set; }

		[DataMember]
		public ushort No { get; set; }

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public string Description { get; set; }

		[DataMember]
		public ushort Fire1Count { get; set; }

		[DataMember]
		public ushort Fire2Count { get; set; }

		[DataMember]
		public List<Guid> DeviceUIDs { get; set; }

		public string PresentationName
		{
			get { return No + "." + Name; }
		}

		public override XBinaryInfo BinaryInfo
		{
			get
			{
				return new XBinaryInfo()
				{
					Type = "Зона",
					Name = Name,
					Address = No.ToString()
				};
			}
		}

		public override string GetBinaryDescription()
		{
			return Name + " - " + No.ToString();
		}
	}
}