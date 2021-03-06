﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FiresecAPI;
using FiresecAPI.Models;

namespace FiresecClient
{
	public partial class FiresecConfiguration
	{
		public string GetCommaSeparatedZones(List<int> zones)
		{
			if (zones.Count > 0)
			{
				var orderedZones = zones.OrderBy(x => x).ToList();
				int prevZoneNo = orderedZones[0];
				List<List<int>> groupOfZones = new List<List<int>>();

				for (int i = 0; i < orderedZones.Count; i++)
				{
					var zoneNo = orderedZones[i];
					var haveZonesBetween = DeviceConfiguration.Zones.Any(x => (x.No > prevZoneNo) && (x.No < zoneNo));
					if (haveZonesBetween)
					{
						groupOfZones.Add(new List<int>() { zoneNo });
					}
					else
					{
						if (groupOfZones.Count == 0)
						{
							groupOfZones.Add(new List<int>() { zoneNo });
						}
						else
						{
							groupOfZones.Last().Add(zoneNo);
						}
					}
					prevZoneNo = zoneNo;
				}

				var presenrationZones = new StringBuilder();
				for (int i = 0; i < groupOfZones.Count; i++)
				{
					var zoneGroup = groupOfZones[i];

					if (i > 0)
						presenrationZones.Append(", ");

					presenrationZones.Append(zoneGroup.First().ToString());
					if (zoneGroup.Count > 1)
					{
						presenrationZones.Append(" - " + zoneGroup.Last().ToString());
					}
				}

				return presenrationZones.ToString();
			}
			return "";
		}

		public string GetPresentationZone(ZoneLogic zoneLogic)
		{
			string result = "";

			for (int i = 0; i < zoneLogic.Clauses.Count; i++)
			{
				var clause = zoneLogic.Clauses[i];

				if (i > 0)
				{
					switch (zoneLogic.JoinOperator)
					{
						case ZoneLogicJoinOperator.And:
							result += " и ";
							break;
						case ZoneLogicJoinOperator.Or:
							result += " или ";
							break;
						default:
							break;
					}
				}

				if (clause.DeviceUID != Guid.Empty)
				{
					result += "Сработка устройства " + clause.Device.PresentationAddress + " - " + clause.Device.Driver.Name;
					continue;
				}

				if (clause.State == ZoneLogicState.Failure)
				{
					result += "состояние неисправность прибора";
					continue;
				}

				result += "состояние " + clause.State.ToDescription();

				string stringOperation = null;
				switch (clause.Operation)
				{
					case ZoneLogicOperation.All:
						stringOperation = "во всех зонах из";
						break;

					case ZoneLogicOperation.Any:
						stringOperation = "в любой зоне из";
						break;

					default:
						break;
				}

				result += " " + stringOperation + " " + GetCommaSeparatedZones(clause.Zones);
			}

			return result;
		}

		public string GetPresentationZone(Device device)
		{
			if (device.Driver.IsZoneDevice)
			{
				var zone = DeviceConfiguration.Zones.FirstOrDefault(x => x.No == device.ZoneNo);
				if (zone != null)
					return zone.PresentationName;
				return "";
			}

			if (device.Driver.IsZoneLogicDevice && device.ZoneLogic != null)
				return GetPresentationZone(device.ZoneLogic);
			if (device.Driver.IsIndicatorDevice && device.IndicatorLogic != null)
				return device.IndicatorLogic.ToString();
			if ((device.Driver.DriverType == DriverType.PDUDirection) && (device.PDUGroupLogic != null))
				return device.PDUGroupLogic.ToString();

			return "";
		}

		public string GetIndicatorString(Device indicatorDevice)
		{
			switch (indicatorDevice.IndicatorLogic.IndicatorLogicType)
			{
				case IndicatorLogicType.Device:
					{
						if (indicatorDevice.IndicatorLogic.DeviceUID != Guid.Empty)
						{
							var deviceString = "Устр: ";
							deviceString += indicatorDevice.IndicatorLogic.Device.PresentationAddressAndDriver;
							return deviceString;
						}
						break;
					}
				case IndicatorLogicType.Zone:
					{
						if (indicatorDevice.IndicatorLogic.Zones.Count > 0)
							return "Зоны: " + GetCommaSeparatedZones(indicatorDevice.IndicatorLogic.Zones);
						break;
					}
			}
			return "";
		}
	}
}