﻿using System;
using System.Linq;
using Firesec.Journals;
using FiresecAPI.Models;
using FiresecAPI;

namespace FiresecDriver
{
	public static class JournalConverter
	{
		public static JournalRecord Convert(journalType innerJournalRecord)
		{
			var journalRecord = new JournalRecord()
			{
				OldId = int.Parse(innerJournalRecord.IDEvents),
				DeviceTime = ConvertTime(innerJournalRecord.Dt),
				SystemTime = ConvertTime(innerJournalRecord.SysDt),
				ZoneName = innerJournalRecord.ZoneName,
				Description = innerJournalRecord.EventDesc,
				DeviceName = innerJournalRecord.CLC_Device,
				PanelName = innerJournalRecord.CLC_DeviceSource,
				DeviceDatabaseId = innerJournalRecord.IDDevices,
				PanelDatabaseId = innerJournalRecord.IDDevicesSource,
				User = innerJournalRecord.UserInfo,
				Detalization = innerJournalRecord.CLC_Detalization,
				SubsystemType = EnumsConverter.StringToSubsystemType(innerJournalRecord.IDSubSystem),
				StateType = (StateType)int.Parse(innerJournalRecord.IDTypeEvents),
			};
			SetDeviceCatogory(journalRecord);

			return journalRecord;
		}

		static void SetDeviceCatogory(JournalRecord journalRecord)
		{
			Device device = null;
			if (ConfigurationCash.DeviceConfiguration.Devices != null)
			{
				if (string.IsNullOrWhiteSpace(journalRecord.DeviceDatabaseId) == false)
				{
					device = ConfigurationCash.DeviceConfiguration.Devices.FirstOrDefault(
						 x => x.DatabaseId == journalRecord.DeviceDatabaseId);
				}
				else
				{
					device = ConfigurationCash.DeviceConfiguration.Devices.FirstOrDefault(
						   x => x.DatabaseId == journalRecord.PanelDatabaseId);
				}
			}
			if (device != null)
			{
				journalRecord.DeviceCategory = (int)device.Driver.Category;
			}
			else
			{
				journalRecord.DeviceCategory = (int)DeviceCategoryType.None;
			}
		}

		public static DateTime ConvertTime(string firesecTime)
		{
			return new DateTime(
				int.Parse(firesecTime.Substring(0, 4)),
				int.Parse(firesecTime.Substring(4, 2)),
				int.Parse(firesecTime.Substring(6, 2)),
				int.Parse(firesecTime.Substring(9, 2)),
				int.Parse(firesecTime.Substring(12, 2)),
				int.Parse(firesecTime.Substring(15, 2))
			);
		}
	}
}