﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace XFiresecAPI
{
	[DataContract]
	public class XDevice : XBinaryBase
	{
		public XDevice()
		{
			UID = Guid.NewGuid();
			Children = new List<XDevice>();
			Properties = new List<XProperty>();
			OutDependenceUIDs = new List<Guid>();
			Zones = new List<Guid>();
			DeviceLogic = new XDeviceLogic();
			PlanElementUIDs = new List<Guid>();
			IsNotUsed = false;
		}

		public XDriver Driver { get; set; }
		public XDevice Parent { get; set; }
		public List<Guid> OutDependenceUIDs { get; set; }

		[DataMember]
		public Guid UID { get; set; }

		[DataMember]
		public Guid DriverUID { get; set; }

		[DataMember]
		public byte ShleifNo { get; set; }

		[DataMember]
		public byte IntAddress { get; set; }

		[DataMember]
		public string Description { get; set; }

		[DataMember]
		public List<XDevice> Children { get; set; }

		[DataMember]
		public List<XProperty> Properties { get; set; }

		[DataMember]
		public List<Guid> Zones { get; set; }

		[DataMember]
		public XDeviceLogic DeviceLogic { get; set; }

		[DataMember]
		public bool IsNotUsed { get; set; }

		[DataMember]
		public List<Guid> PlanElementUIDs { get; set; }

		public string Address
		{
			get
			{
				if (Driver.HasAddress == false)
					return "";

				if (Driver.IsDeviceOnShleif == false)
					return IntAddress.ToString();

				return ShleifNo.ToString() + "." + IntAddress.ToString();
			}
		}

		public string DottedAddress
		{
			get
			{
				var address = new StringBuilder();
				foreach (var parentDevice in AllParents.Where(x => x.Driver.HasAddress))
				{
					if (parentDevice.Driver.IsGroupDevice)
						continue;

					address.Append(parentDevice.Address);
					address.Append(".");
				}
				if (Driver.HasAddress)
				{
					address.Append(Address);
					address.Append(".");
				}

				if (address.Length > 0 && address[address.Length - 1] == '.')
					address.Remove(address.Length - 1, 1);

				return address.ToString();
			}
		}

		public string PresentationAddressAndDriver
		{
			get
			{
				if (Driver.HasAddress)
					return Address + " - " + Driver.Name;
				return Driver.Name;
			}
		}

		public string ShortPresentationAddressAndDriver
		{
			get
			{
				if (Driver.HasAddress)
					return Address + " - " + Driver.ShortName;
				return Driver.Name;
			}
		}

		public string PresentationDriverAndAddress
		{
			get
			{
				if (Driver.DriverType == XDriverType.GK)
					return Driver.ShortName + " " + GetGKIpAddress();

				if (Driver.HasAddress)
					return Driver.ShortName + " - " + Address;
				return Driver.ShortName;
			}
		}

		public void SetAddress(string address)
		{
			try
			{
				if (Driver.HasAddress == false)
					return;

				if (Driver.IsDeviceOnShleif == false)
				{
					IntAddress = byte.Parse(address);
					return;
				}

				var addresses = address.Split('.');
				byte shleifPart = byte.Parse(addresses[0]);
				byte addressPart = byte.Parse(addresses[1]);

				ShleifNo = shleifPart;
				IntAddress = addressPart;

				if (Driver.IsGroupDevice)
				{
					for (int i = 0; i < Children.Count; i++)
					{
						Children[i].ShleifNo = ShleifNo;
						Children[i].IntAddress = (byte)(IntAddress + i);
					}
				}
			}
			catch { }
		}

		public bool CanEditAddress
		{
			get
			{
				if (Parent != null && Parent.Driver.IsGroupDevice)
					return false;
				return (Driver.HasAddress && Driver.CanEditAddress);
			}
		}

		public List<XDevice> AllParents
		{
			get
			{
				if (Parent == null)
					return new List<XDevice>();

				List<XDevice> allParents = Parent.AllParents;
				allParents.Add(Parent);
				return allParents;
			}
		}

		public override XBinaryInfo BinaryInfo
		{
			get
			{
				return new XBinaryInfo()
				{
					Type = "Устройство",
					Name = Driver.ShortName,
					Address = Address
				};
			}
		}

		public override string GetBinaryDescription()
		{
			switch (Driver.DriverType)
			{
				case XDriverType.GKIndicator:
				case XDriverType.GKLine:
				case XDriverType.GKRele:
					return Description;
			}
			if (Driver.HasAddress)
				return Driver.ShortName + " - " + DottedAddress;
			return Driver.ShortName;
		}

		public string GetGKIpAddress()
		{
			if (Driver.DriverType == XDriverType.GK)
			{
				var ipProperty = this.Properties.FirstOrDefault(x => x.Name == "IPAddress");
				if (ipProperty != null)
				{
					return ipProperty.StringValue;
				}
			}
			return null;
		}

		public bool IsRealDevice
		{
			get
			{
				if (Driver.IsGroupDevice)
					return false;
				if (Driver.DriverType == XDriverType.System)
					return false;
				return true;
			}
		}
	}
}