﻿using System.Collections.Generic;
using System.Linq;
using Common;
using FiresecAPI.Models;
using FiresecClient;
using ReportsModule.Models;

namespace ReportsModule.Reports
{
    public class ReportDevicesListDataTable
    {
        public string RdlcFileName = "ReportDeviceListRDLC.rdlc";
        public string DataTableName = "DataSetDataSetDeviceList";

        public ReportDevicesListDataTable()
        {
            _devicesList = new List<ReportDeviceListModel>();
        }

        public void Initialize()
        {
            _devicesList = new List<ReportDeviceListModel>();
            if (FiresecManager.DeviceConfiguration.Devices.IsNotNullOrEmpty())
            {
                string type = "";
                string address = "";
                string zonePresentationName = "";
                foreach (var device in FiresecManager.DeviceConfiguration.Devices)
                {
                    zonePresentationName = "";
                    type = device.Driver.ShortName;
                    address = device.DottedAddress;
                    if (device.Driver.IsZoneDevice)
                    {

                        if (FiresecManager.DeviceConfiguration.Zones.IsNotNullOrEmpty())
                        {
                            var zone = FiresecManager.DeviceConfiguration.Zones.FirstOrDefault(x => x.No == device.ZoneNo);
                            if (zone != null)
                            {
                                zonePresentationName = zone.PresentationName;
                            }
                        }
                    }

                    if (device.Driver.DriverType == DriverType.Indicator)
                    {
                        if (device.IndicatorLogic.Zones.Count == 1)
                        {
                            var zone = FiresecManager.DeviceConfiguration.Zones.FirstOrDefault(x => x.No == device.IndicatorLogic.Zones[0]);
                            string presentationName = "";
                            if (zone != null)
                            {
                                presentationName = zone.PresentationName;
                            }
                            zonePresentationName = "Зоны: " + presentationName;
                        }
                        else
                        {
                            zonePresentationName = device.IndicatorLogic.ToString();
                        }
                    }

                    if (device.Driver.DriverType == DriverType.Page)
                    {
                        address = device.IntAddress.ToString();
                    }
                    if (device.Driver.DriverType == DriverType.PumpStation)
                    {

                    }
                    _devicesList.Add(new ReportDeviceListModel()
                        {
                            Type = type,
                            Address = address,
                            ZoneName = zonePresentationName
                        });
                }
            }
        }

        List<ReportDeviceListModel> _devicesList;
        public List<ReportDeviceListModel> DevicesList
        {
            get { return new List<ReportDeviceListModel>(_devicesList); }
            protected set { _devicesList = value; }
        }

    }
}