﻿using System;
using System.Runtime.Serialization;
using Infrustructure.Plans.Elements;

namespace FiresecAPI.Models
{
	[DataContract]
	public class ElementDevice : ElementBasePoint, IElementZLayer
	{
		public ElementDevice()
		{
			DeviceUID = Guid.Empty;
		}

		[DataMember]
		public Guid DeviceUID { get; set; }

		public override ElementBase Clone()
		{
			ElementDevice elementBase = new ElementDevice()
			{
				DeviceUID = DeviceUID
			};
			Copy(elementBase);
			return elementBase;
		}

		#region IElementZLayer Members

		public int ZLayerIndex
		{
			get { return 5; }
		}

		#endregion
	}
}