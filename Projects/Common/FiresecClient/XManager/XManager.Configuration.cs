﻿using System.Collections.Generic;
using System.Linq;
using XFiresecAPI;

namespace FiresecClient
{
	public partial class XManager
	{
		public static void Invalidate()
		{
			InitializeMissingDefaultProperties();
			InitializeDevicesInZone();
			InitializeZoneLogic();
			InitializeDirectionZones();
			InitializeDirectionDevices();
		}

		public static void GetConfiguration()
		{
			DeviceConfiguration = FiresecManager.FiresecService.GetXDeviceConfiguration();
			UpdateConfiguration();
		}

		static void InitializeDevicesInZone()
		{
			foreach (var zone in DeviceConfiguration.Zones)
			{
				zone.Devices = new List<XDevice>();
				for (int i = zone.DeviceUIDs.Count - 1; i >= 0; i--)
				{
					var deviceUID = zone.DeviceUIDs[i];
					var device = DeviceConfiguration.Devices.FirstOrDefault(x => x.UID == deviceUID);
					if (device != null)
					{
						zone.Devices.Add(device);
					}
					else
					{
						zone.DeviceUIDs.Remove(deviceUID);
					}
				}
			}
		}

		static void InitializeZoneLogic()
		{
			foreach (var device in DeviceConfiguration.Devices)
			{
				for (int stateLogicIndex = device.DeviceLogic.StateLogics.Count - 1; stateLogicIndex >= 0; stateLogicIndex--)
				{
					var stateLogic = device.DeviceLogic.StateLogics[stateLogicIndex];
					for (int clauseIndex = stateLogic.Clauses.Count - 1; clauseIndex >= 0; clauseIndex--)
					{
						var clause = stateLogic.Clauses[clauseIndex];
						clause.XDevices = new List<XDevice>();
						for (int deviceIndex = clause.Devices.Count - 1; deviceIndex >= 0; deviceIndex--)
						{
							var deviceUID = clause.Devices[deviceIndex];
							var clauseDevice = DeviceConfiguration.Devices.FirstOrDefault(x => x.UID == deviceUID);
							if (clauseDevice != null)
							{
								clause.XDevices.Add(clauseDevice);
							}
							else
							{
								clause.Devices.Remove(deviceUID);
							}
						}
						clause.XZones = new List<XZone>();
						for (int zoneIndex = clause.Zones.Count - 1; zoneIndex >= 0; zoneIndex--)
						{
							var zoneUID = clause.Zones[zoneIndex];
							var clauseZone = DeviceConfiguration.Zones.FirstOrDefault(x => x.UID == zoneUID);
							if (clauseZone != null)
							{
								clause.XZones.Add(clauseZone);
							}
							else
							{
								clause.Zones.Remove(zoneUID);
							}
						}
						if ((clause.XDevices.Count == 0) && (clause.XZones.Count == 0))
							stateLogic.Clauses.Remove(clause);
					}
					if (stateLogic.Clauses.Count == 0)
						device.DeviceLogic.StateLogics.Remove(stateLogic);

				}
			}
		}

		static void InitializeDirectionZones()
		{
			foreach (var direction in DeviceConfiguration.Directions)
			{
				direction.XZones = new List<XZone>();
				for (int i = direction.Zones.Count - 1; i >= 0; i--)
				{
					var zoneUID = direction.Zones[i];
					var zone = DeviceConfiguration.Zones.FirstOrDefault(x => x.UID == zoneUID);
					if (zone != null)
						direction.XZones.Add(zone);
					else
						direction.Zones.Remove(zoneUID);
				}
			}
		}

		static void InitializeDirectionDevices()
		{
			foreach (var direction in DeviceConfiguration.Directions)
			{
				for (int i = direction.DirectionDevices.Count - 1; i >= 0; i--)
				{
					var directionDevice = direction.DirectionDevices[i];
					var device = DeviceConfiguration.Devices.FirstOrDefault(x => x.UID == directionDevice.DeviceUID);
					directionDevice.Device = device;
					if (device == null)
						direction.DirectionDevices.Remove(directionDevice);
				}
			}
		}
	}
}