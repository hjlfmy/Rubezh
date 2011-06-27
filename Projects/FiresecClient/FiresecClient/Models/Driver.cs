﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Firesec.Metadata;
using FiresecApi;

namespace FiresecClient.Models
{
    public class Driver
    {
        public static Firesec.Metadata.config Metadata;
        public configDrv _driver;

        public Driver(configDrv driver)
        {
            _driver = driver;
        }

        public List<DriverProperty> Properties
        {
            get
            {
                List<DriverProperty> Properties = new List<DriverProperty>();
                if (_driver.propInfo != null)
                {
                    foreach (var firesecProperty in _driver.propInfo)
                    {
                        if (firesecProperty.hidden == "1")
                            continue;
                        if ((firesecProperty.caption == "Заводской номер") || (firesecProperty.caption == "Версия микропрограммы"))
                            continue;

                        DriverProperty driverProperty = new DriverProperty(firesecProperty);
                        Properties.Add(driverProperty);
                    }
                }
                return Properties;
            }
        }

        public List<configDrvParamInfo> Parameters
        {
            get
            {
                if (_driver.paramInfo != null)
                {
                    return new List<configDrvParamInfo>(_driver.paramInfo);
                }
                return new List<configDrvParamInfo>();
            }
        }

        public List<configDrvState> States
        {
            get
            {
                if (_driver.state != null)
                {
                    return new List<configDrvState>(_driver.state);
                }
                return new List<configDrvState>();
            }
        }

        public string Id
        {
            get { return _driver.id; }
        }

        public string Name
        {
            get { return _driver.name; }
        }

        public string ShortName
        {
            get { return _driver.shortName; }
        }

        public bool HasAddress
        {
            get { return _driver.ar_no_addr != "1"; }
        }

        public bool CanEditAddress
        {
            get
            {
                if (_driver.ar_no_addr != null)
                {
                    if (_driver.ar_no_addr == "1")
                        return false;

                    if (_driver.acr_enabled == "1")
                        return false;
                }
                return true;
            }
        }

        public string AddressMask
        {
            get { return _driver.addrMask; }
        }

        public string ChildAddressMask
        {
            get { return _driver.childAddrMask; }
        }

        public int ShleifCount
        {
            get
            {
                if (_driver.childAddrMask != null)
                {
                    switch (_driver.childAddrMask)
                    {
                        case "[8(1)-15(2)];[0(1)-7(255)]":
                            return 2;

                        case "[8(1)-15(4)];[0(1)-7(255)]":
                            return 4;

                        case "[8(1)-15(10)];[0(1)-7(255)]":
                            return 10;
                    }
                }
                return 0;
            }
        }

        public List<string> AvaliableChildren
        {
            get
            {
                List<string> drivers = new List<string>();

                foreach (var childDriver in Metadata.drv)
                {
                    var childClass = Metadata.@class.FirstOrDefault(x => x.clsid == childDriver.clsid);
                    if ((childClass.parent != null) && (childClass.parent.Any(x => x.clsid == _driver.clsid)))
                    {
                        if ((childDriver.lim_parent != null) && (childDriver.lim_parent != _driver.id))
                            continue;
                        if (childDriver.acr_enabled == "1")
                            continue;

                        drivers.Add(childDriver.id);
                    }
                }

                return drivers;
            }
        }

        public List<string> AutoCreateChildren
        {
            get
            {
                List<string> drivers = new List<string>();

                foreach (var childDriver in Metadata.drv)
                {
                    var childClass = Metadata.@class.FirstOrDefault(x => x.clsid == childDriver.clsid);
                    if ((childClass.parent != null) && (childClass.parent.Any(x => x.clsid == _driver.clsid)))
                    {
                        if ((childDriver.lim_parent != null) && (childDriver.lim_parent != _driver.id))
                            continue;

                        if (childDriver.acr_enabled == "1")
                            drivers.Add(childDriver.id);
                    }
                }

                return drivers;
            }
        }

        public bool IsRangeEnabled
        {
            get { return _driver.ar_enabled == "1"; }
        }

        public int MinAddress
        {
            get { return Convert.ToInt32(_driver.ar_from); }
        }

        public int MaxAddress
        {
            get { return Convert.ToInt32(_driver.ar_to); }
        }

        public bool IsAutoCreate
        {
            get { return _driver.acr_enabled == "1"; }
        }

        public int MinAutoCreateAddress
        {
            get { return Convert.ToInt32(_driver.acr_from); }
        }

        public int MaxAutoCreateAddress
        {
            get { return Convert.ToInt32(_driver.acr_to); }
        }

        public bool HasAddressMask
        {
            get { return _driver.addrMask != null; }
        }

        public string ImageSource
        {
            get
            {
                string imageSource;
                if (!string.IsNullOrEmpty(_driver.dev_icon))
                {
                    imageSource = _driver.dev_icon;
                }
                else
                {
                    var metadataClass = Metadata.@class.FirstOrDefault(x => x.clsid == _driver.clsid);
                    imageSource = metadataClass.param.FirstOrDefault(x => x.name == "Icon").value;
                }

                return @"C:/Program Files/Firesec/Icons/" + imageSource + ".ico";
            }
        }

        public bool HasImage
        {
            get { return ImageSource != @"C:/Program Files/Firesec/Icons/Device_Device.ico"; }
        }

        public bool CanAddChildren
        {
            get
            {
                List<Firesec.Metadata.configDrv> childDrivers = new List<Firesec.Metadata.configDrv>();

                foreach (var childDriver in Metadata.drv)
                {
                    var childClass = Metadata.@class.FirstOrDefault(x => x.clsid == childDriver.clsid);
                    if ((childClass.parent != null) && (childClass.parent.Any(x => x.clsid == _driver.clsid)))
                    {
                        if ((childDriver.lim_parent != null) && (childDriver.lim_parent != _driver.id))
                            continue;
                        if (childDriver.acr_enabled == "1")
                            continue;
                        childDrivers.Add(childDriver);
                    }
                }

                return (childDrivers.Count > 0);
            }
        }

        public bool IsZoneDevice
        {
            get { return !((_driver.minZoneCardinality == "0") && (_driver.maxZoneCardinality == "0")); }
        }

        public bool IsZoneLogicDevice
        {
            get { return ((_driver.options != null) && (_driver.options.Contains("ExtendedZoneLogic"))); }
        }

        public bool CanDisable
        {
            get { return (_driver.options != null) && (_driver.options.Contains("Ignorable")); }
        }

        public bool IsPlaceable
        {
            get { return (_driver.options != null) && (_driver.options.Contains("Placeable")); }
        }

        public bool IsIndicatorDevice
        {
            get { return (_driver.name == "Индикатор"); }
        }

        public bool CanControl
        {
            get { return (DriverName == "Задвижка"); }
        }

        public DeviceCategory Category
        {
            get
            {
                switch (_driver.cat)
                {
                    case "0":
                        return DeviceCategory.Other;

                    case "1":
                        return DeviceCategory.Device;

                    case "2":
                        return DeviceCategory.Sensor;

                    case "3":
                        return DeviceCategory.Effector;

                    case "4":
                        return DeviceCategory.Communication;

                    case "5":
                        return DeviceCategory.None;

                    case "6":
                        return DeviceCategory.RemoteServer;

                    default:
                        return DeviceCategory.None;
                }
            }
        }

        public string CategoryName
        {
            get
            {
                switch (Category)
                {
                    case DeviceCategory.Other:
                        return "Прочие устройства";

                    case DeviceCategory.Device:
                        return "Приборы";

                    case DeviceCategory.Sensor:
                        return "Датчики";

                    case DeviceCategory.Effector:
                        return "ИУ";

                    case DeviceCategory.Communication:
                        return "Сеть передачи данных";

                    case DeviceCategory.None:
                        return "Не указано";

                    case DeviceCategory.RemoteServer:
                        return "Удаленный сервер";

                    default:
                        return "";
                }
            }
        }

        public DeviceType DeviceType
        {
            get
            {
                if (_driver.options != null)
                {
                    if (_driver.options.Contains("FireOnly"))
                        return FiresecApi.DeviceType.Fire;

                    if (_driver.options.Contains("SecOnly"))
                        return FiresecApi.DeviceType.Sequrity;

                    if (_driver.options.Contains("TechOnly"))
                        return FiresecApi.DeviceType.Technoligical;
                }

                return FiresecApi.DeviceType.FireSecurity;
            }
        }

        public string DeviceTypeName
        {
            get
            {
                switch (DeviceType)
                {
                    case FiresecApi.DeviceType.Fire:
                        return "пожарный";

                    case FiresecApi.DeviceType.Sequrity:
                        return "охранный";

                    case FiresecApi.DeviceType.Technoligical:
                        return "технологический";

                    case FiresecApi.DeviceType.FireSecurity:
                        return "охранно-пожарный";

                    default:
                        return "";
                }
            }
        }

        class DriverData
        {
            public DriverData(string DriverId, int IgnoreLevel, string Name)
            {
                this.Name = Name;
                this.DriverId = DriverId;
                this.IgnoreLevel = IgnoreLevel;
            }

            public string Name { get; set; }
            public string DriverId { get; set; }
            public int IgnoreLevel { get; set; }
        }

        static List<DriverData> driverDataList;
        static Driver()
        {
            driverDataList = new List<DriverData>();
            driverDataList.Add(new DriverData("80A37AF1-B1AD-45D5-A34C-6FA2960F9706", 2, "Виртуальная панель"));
            driverDataList.Add(new DriverData("743CEBD1-B91D-4521-9B02-1E674F94789A", 2, "Виртуальный порт"));
            driverDataList.Add(new DriverData("F8340ECE-C950-498D-88CD-DCBABBC604F3", 0, "Компьютер"));
            driverDataList.Add(new DriverData("{0695ADC6-4D28-44D4-8E24-7F13D91F62ED}", 2, "COM порт (V1)"));
            driverDataList.Add(new DriverData("{07C5D4D8-19AC-4786-832A-7A81ACCE364C}", 2, "Прибор Рубеж 10A"));
            driverDataList.Add(new DriverData("8CE7A914-4FF2-41F2-B991-70E84228D38D", 2, "Прибор Рубеж 2A"));
            driverDataList.Add(new DriverData("{FD91CD1A-4F3B-4F76-AA74-AB9C8B9E79F3}", 2, "Пожарный комбинированный извещатель ИП212&#047;101-64-А2R1"));
            driverDataList.Add(new DriverData("{F8EBE5F5-A012-4DB7-B300-49552B458931}", 2, "Пожарный дымовой извещатель ИП 212-64"));
            driverDataList.Add(new DriverData("{E613E421-68A2-4A31-96CC-B9CAB7D64216}", 2, "Пожарный тепловой извещатель ИП 101-29-A3R1"));
            driverDataList.Add(new DriverData("{4F83823A-2C4E-4F4E-BF67-12EFC82B4FEC}", 2, "Ручной извещатель ИПР514-3"));
            driverDataList.Add(new DriverData("{AB9C8B4C-43CA-44BB-86DA-527F0D8B2F75}", 2, "Пожарная адресная метка АМ1"));
            driverDataList.Add(new DriverData("50CDD49E-4981-475C-9083-ADB79458B0B0", 2, "Метка контроля питания"));
            driverDataList.Add(new DriverData("75D4399D-EC01-42E0-B77E-31F5E1248905", 2, "Релейный исполнительный модуль РМ-1"));
            driverDataList.Add(new DriverData("{C87E5BBD-2E0C-4213-84D0-2376DB27BDF2}", 2, "АСПТ"));
            driverDataList.Add(new DriverData("ABDE5AF2-2B77-4421-879C-2A14E7F056B2", 2, "COM порт (V2)"));
            driverDataList.Add(new DriverData("6298807D-850B-4C65-8792-A4EAB2A4A72A", 0, "Страница"));
            driverDataList.Add(new DriverData("E486745F-6130-4027-9C01-465DE5415BBF", 0, "Индикатор"));
            driverDataList.Add(new DriverData("B476541B-5298-4B3E-A9BA-605B839B1011", 0, "Прибор Рубеж-2AM"));
            driverDataList.Add(new DriverData("02CE2CC4-D71F-4EAA-ACCC-4F2E870F548C", 0, "БУНС"));
            driverDataList.Add(new DriverData("F966D47B-468D-40A5-ACA7-9BE30D0A3847", 0, "Модуль сопряжения МС-3"));
            driverDataList.Add(new DriverData("{868ED643-0ED6-48CD-A0E0-4AD46104C419}", 0, "Модуль сопряжения МС-4"));
            driverDataList.Add(new DriverData("{584BC59A-28D5-430B-90BF-592E40E843A6}", 0, "Модуль доставки сообщений"));
            driverDataList.Add(new DriverData("28A7487A-BA32-486C-9955-E251AF2E9DD4", 0, "Блок индикации"));
            driverDataList.Add(new DriverData("E750EF8F-54C3-4B00-8C72-C7BEC9E59BFC", 0, "Прибор Рубеж-10AM"));
            driverDataList.Add(new DriverData("F3485243-2F60-493B-8A4E-338C61EF6581", 0, "Прибор Рубеж-4A"));
            driverDataList.Add(new DriverData("96CDBD7E-29F6-45D4-9028-CF10332FAB1A", 1, "Прибор Рубеж-2ОП"));
            driverDataList.Add(new DriverData("4A60242A-572E-41A8-8B87-2FE6B6DC4ACE", 0, "Релейный исполнительный модуль РМ-1"));
            driverDataList.Add(new DriverData("33A85F87-E34C-45D6-B4CE-A4FB71A36C28", 0, "Модуль пожаротушения"));
            driverDataList.Add(new DriverData("1E045AD6-66F9-4F0B-901C-68C46C89E8DA", 0, "Пожарный дымовой извещатель ИП 212-64"));
            driverDataList.Add(new DriverData("799686B6-9CFA-4848-A0E7-B33149AB940C", 0, "Пожарный тепловой извещатель ИП 101-29-A3R1"));
            driverDataList.Add(new DriverData("37F13667-BC77-4742-829B-1C43FA404C1F", 0, "Пожарный комбинированный извещатель ИП212//101-64-А2R1"));
            driverDataList.Add(new DriverData("DBA24D99-B7E1-40F3-A7F7-8A47D4433392", 0, "Пожарная адресная метка АМ1"));
            driverDataList.Add(new DriverData("CD7FCB14-F808-415C-A8B7-11C512C275B4", 0, "Кнопка останова СПТ"));
            driverDataList.Add(new DriverData("E8C04507-0C9D-429C-9BBE-166C3ECA4B5C", 0, "Кнопка запуска СПТ"));
            driverDataList.Add(new DriverData("1909EBDF-467D-4565-AD5C-CD5D9084E4C3", 0, "Кнопка управления автоматикой"));
            driverDataList.Add(new DriverData("2F875F0C-54AA-47CE-B639-FE5E3ED9841B", 1, "Кнопка вкл автоматики ШУЗ и насосов в направлении"));
            driverDataList.Add(new DriverData("032CDF7B-6787-4612-B3D1-03E0D3FD2F53", 1, "Кнопка выкл автоматики ШУЗ и насосов в направлении"));
            driverDataList.Add(new DriverData("935B0020-889B-4A94-9563-EC0E4127E8E3", 1, "Кнопка разблокировки автоматики ШУЗ в направлении"));
            driverDataList.Add(new DriverData("641FA899-FAA0-455B-B626-646E5FBE785A", 0, "Ручной извещатель ИПР513-11"));
            driverDataList.Add(new DriverData("EFCA74B2-AD85-4C30-8DE8-8115CC6DFDD2", 1, "Охранная адресная метка АМ1-О"));
            driverDataList.Add(new DriverData("44EEDF03-0F4C-4EBA-BD36-28F96BC6B16E", 0, "Модуль Управления Клапанами Дымоудаления"));
            driverDataList.Add(new DriverData("B603CEBA-A3BF-48A0-BFC8-94BF652FB72A", 0, "Модуль Управления Клапанами Огнезащиты"));
            driverDataList.Add(new DriverData("AF05094E-4556-4CEE-A3F3-981149264E89", 0, "Насосная Станция"));
            driverDataList.Add(new DriverData("8BFF7596-AEF4-4BEE-9D67-1AE3DC63CA94", 0, "Насос"));
            driverDataList.Add(new DriverData("68E8E353-8CFC-4C54-A1A8-D6B6BF4FD20F", 0, "Жокей-насос"));
            driverDataList.Add(new DriverData("ED58E7EB-BA88-4729-97FF-427EBC822E81", 0, "Компрессор"));
            driverDataList.Add(new DriverData("8AFC9569-9725-4C27-8815-18167642CA29", 0, "Дренажный насос"));
            driverDataList.Add(new DriverData("40DAB36C-2353-4BFD-A1FE-8F542EC15D49", 0, "Насос компенсации утечек"));
            driverDataList.Add(new DriverData("D8997F3B-64C4-4037-B176-DE15546CE568", 0, "Пожарная адресная метка АМП-4"));
            driverDataList.Add(new DriverData("2D078D43-4D3B-497C-9956-990363D9B19B", 0, "Модуль речевого оповещения"));
            driverDataList.Add(new DriverData("4935848F-0084-4151-A0C8-3A900E3CB5C5", 0, "Задвижка"));
            driverDataList.Add(new DriverData("F5A34CE2-322E-4ED9-A75F-FC8660AE33D8", 0, "Технологическая адресная метка АМ1-Т"));
            driverDataList.Add(new DriverData("FD200EDF-94A4-4560-81AA-78C449648D45", 0, "АСПТ"));
            driverDataList.Add(new DriverData("043FBBE0-8733-4C8D-BE0C-E5820DBF7039", 0, "Модуль дымоудаления-1.02//3"));
            driverDataList.Add(new DriverData("05323D14-9070-44B8-B91C-BE024F10E267", 0, "Выход"));
            driverDataList.Add(new DriverData("AB3EF7B1-68AD-4A1B-88A8-997357C3FC5B", 1, "Модуль радиоканала МРК-30"));
            driverDataList.Add(new DriverData("D57CDEF3-ACBC-4773-955E-22A1F016D025", 1, "Ручной радиоканальный извещатель ИПР513-11"));
            driverDataList.Add(new DriverData("CFD407D1-5D19-43EC-9650-A86EC4422EC6", 1, "Пожарный дымовой радиоканальный извещатель ИП 212-64Р"));
            driverDataList.Add(new DriverData("CD0E9AA0-FD60-48B8-B8D7-F496448FADE6", 0, "USB преобразователь МС-2"));
            driverDataList.Add(new DriverData("FDECE1B6-A6C6-4F89-BFAE-51F2DDB8D2C6", 0, "USB преобразователь МС-1"));
            driverDataList.Add(new DriverData("F36B2416-CAF3-4A9D-A7F1-F06EB7AAA76E", 0, "USB Канал МС-2"));
            driverDataList.Add(new DriverData("780DE2E6-8EDD-4CFA-8320-E832EB699544", 0, "USB Канал МС-1"));
            driverDataList.Add(new DriverData("2863E7A3-5122-47F8-BB44-4358450CD0EE", 0, "Канал с резервированием"));
            driverDataList.Add(new DriverData("C2E0F845-D836-4AAE-9894-D5CBE2B9A7DD", 0, "Состав"));
            driverDataList.Add(new DriverData("B9680002-511D-4505-9EF6-0C322E61135F", 0, "USB Канал"));
            driverDataList.Add(new DriverData("1EDE7282-0003-424E-B76C-BB7B413B4F3B", 1, "USB Рубеж-2AM"));
            driverDataList.Add(new DriverData("7CED3D07-C8AF-4141-8D3D-528050EEA72D", 1, "USB Рубеж-4A"));
            driverDataList.Add(new DriverData("39DBC715-C4B5-4AE6-A809-4F214BBBD6C1", 1, "USB Рубеж-2ОП"));
            driverDataList.Add(new DriverData("4A3D1FA3-4F13-44D8-B9AD-825B53416A71", 1, "USB БУНС"));
            driverDataList.Add(new DriverData("zone", 0, "zone"));
            driverDataList.Add(new DriverData("monitor", 0, "monitor"));
        }

        public static string GetDriverNameById(string driverId)
        {
            var driver = driverDataList.FirstOrDefault(x => (x.DriverId == driverId) && (x.IgnoreLevel < 2));
            if (driver == null)
            {
                return null;
            }
            return driver.Name;
        }

        public bool IsIgnore
        {
            get { return (driverDataList.FirstOrDefault(x => (x.DriverId == _driver.id)).IgnoreLevel > 0); }
        }

        public string DriverName
        {
            get
            {
                var driverData = driverDataList.FirstOrDefault(x => (x.DriverId == _driver.id) && (x.IgnoreLevel < 2));
                if (driverData == null)
                {
                    return null;
                }
                return driverData.Name;
            }
        }
    }
}