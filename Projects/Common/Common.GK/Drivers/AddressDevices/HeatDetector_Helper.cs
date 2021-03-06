﻿using System.Linq;
using XFiresecAPI;
using System;

namespace Common.GK
{
	public class HeatDetector_Helper
	{
		public static XDriver Create()
		{
			var driver = new XDriver()
			{
				DriverTypeNo = 0x62,
				DriverType = XDriverType.HeatDetector,
				UID = new Guid("799686b6-9cfa-4848-a0e7-b33149ab940c"),
				Name = "Пожарный тепловой извещатель ИП 101-29-A3R1",
				ShortName = "ИП-29",
				HasZone = true
			};

			GKDriversHelper.AddAvailableStates(driver, XStateType.Test);
			GKDriversHelper.AddAvailableStates(driver, XStateType.Fire1);

			GKDriversHelper.AddIntProprety(driver, 0x8B, "Порог срабатывания по температуре", 0, 65, 0, 85);
			GKDriversHelper.AddIntProprety(driver, 0x8C, "Порог срабатывания по градиенту температуры", 0, 100, 0, 255);

			return driver;
		}
	}
}