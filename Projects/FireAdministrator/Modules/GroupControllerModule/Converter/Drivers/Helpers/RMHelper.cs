﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XFiresecAPI;
using FiresecClient;

namespace GKModule.Converter
{
	public static class RMHelper
	{
		public static void Create()
		{
			var xDriver = XManager.DriversConfiguration.Drivers.FirstOrDefault(x=>x.DriverType == XDriverType.RM_1);

			var property1 = new XDriverProperty()
			{
				No = 0x82,
				Name = "Конфигурация релейного модуля",
				Caption = "Конфигурация релейного модуля",
				Default = 1,
				AlternativePareterNames = new List<string>() { "Стоп", "Пуск" }
			};
			var driverPropertyParameter1 = new XDriverPropertyParameter()
			{
				Name = "Разомкнутые контакты",
				AlternativeName = "замкнутые контакты в соответствии с задержками",
				Value = 1
			};
			var driverPropertyParameter2 = new XDriverPropertyParameter()
			{
				Name = "Разомкнутые контакты",
				AlternativeName = "режим «мерцания» с частотой 0,5Гц в соответствии с задержками",
				Value = 2
			};
			var driverPropertyParameter3 = new XDriverPropertyParameter()
			{
				Name = "замкнутые контакты",
				AlternativeName = "Разомкнутые контакты, в соответствии с задержками",
				Value = 3
			};
			var driverPropertyParameter4 = new XDriverPropertyParameter()
			{
				Name = "замкнутые контакты",
				AlternativeName = "режим «мерцания» с частотой 0,5Гц в соответствии с задержками",
				Value = 4
			};
			var driverPropertyParameter5 = new XDriverPropertyParameter()
			{
				Name = "режим «мерцания» с частотой 0,5Гц ",
				AlternativeName = "Разомкнутые контакты, в соответствии с задержками",
				Value = 5
			};
			var driverPropertyParameter6 = new XDriverPropertyParameter()
			{
				Name = "режим «мерцания» с частотой 0,5Гц ",
				AlternativeName = "замкнутые контакты в соответствии с задержками",
				Value = 6
			};
			property1.Parameters.Add(driverPropertyParameter1);
			property1.Parameters.Add(driverPropertyParameter2);
			property1.Parameters.Add(driverPropertyParameter3);
			property1.Parameters.Add(driverPropertyParameter4);
			property1.Parameters.Add(driverPropertyParameter5);
			property1.Parameters.Add(driverPropertyParameter6);
			xDriver.Properties.Add(property1);

			var property2 = new XDriverProperty()
			{
				No = 0x83,
				Name = "Конфигурация релейного модуля",
				Caption = "Конфигурация релейного модуля"
			};

			xDriver.Properties.Add(property2);
		}
	}
}