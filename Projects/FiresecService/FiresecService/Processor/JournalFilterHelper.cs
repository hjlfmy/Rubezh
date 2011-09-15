﻿using System.Linq;
using Common;
using FiresecAPI.Models;

namespace FiresecService.Processor
{
    public class JournalFilterHelper
    {
        public static bool FilterRecord(JournalFilter journalFilter, JournalRecord journalRecord)
        {
            bool result = true;
            if (journalFilter.Categories.IsNotNullOrEmpty())
            {
                var device = FiresecManager.DeviceConfiguration.Devices.FirstOrDefault(
                        x => x.DatabaseId == journalRecord.DeviceDatabaseId ||
                             x.DatabaseId == journalRecord.PanelDatabaseId);

                if ((result = (device != null)))
                {
                    result = journalFilter.Categories.Any(daviceCategory => daviceCategory == device.Driver.Category);
                }
            }

            if (result && journalFilter.Events.IsNotNullOrEmpty())
            {
                result = journalFilter.Events.Any(_event => _event == journalRecord.StateType);
            }

            return result;
        }
    }
}