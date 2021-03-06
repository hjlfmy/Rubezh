﻿using XFiresecAPI;
using System.Collections.Generic;

namespace Common.GK
{
	public static class GKDriversHelper
	{
		public static void AddPlainEnumProprety(XDriver xDriver, byte no, string propertyName, byte offset, string parameter1Name, string parameter2Name, int startValue = 0)
		{
			var property = new XDriverProperty()
			{
				No = no,
				Name = propertyName,
				Caption = propertyName,
				Default = 0,
				Offset = offset
			};
			var parameter1 = new XDriverPropertyParameter()
			{
				Name = parameter1Name,
				Value = (ushort)startValue
			};
			var parameter2 = new XDriverPropertyParameter()
			{
				Name = parameter2Name,
				Value = (ushort)(startValue + 1)
			};
			property.Parameters.Add(parameter1);
			property.Parameters.Add(parameter2);
			xDriver.Properties.Add(property);
		}

		public static void AddBoolProprety(XDriver xDriver, byte no, string propertyName, byte offset)
		{
			var property = new XDriverProperty()
			{
				No = no,
				Name = propertyName,
				Caption = propertyName,
				DriverPropertyType = XDriverPropertyTypeEnum.BoolType,
				Offset = offset
			};
			xDriver.Properties.Add(property);
		}

		public static void AddIntProprety(XDriver xDriver, byte no, string propertyName, byte offset, int defaultValue, int min, int max)
		{
			var property = new XDriverProperty()
			{
				No = no,
				Name = propertyName,
				Caption = propertyName,
				DriverPropertyType = XDriverPropertyTypeEnum.IntType,
				Offset = offset,
				Default = (ushort)defaultValue,
				Min = (ushort)min,
				Max = (ushort)max
			};
			xDriver.Properties.Add(property);
		}

		public static void AddPropertyParameter(XDriverProperty property, string name, int value)
		{
			var parameter = new XDriverPropertyParameter()
			{
				Name = name,
				Value = (ushort)value
			};
			property.Parameters.Add(parameter);
		}

		public static void AddAvailableStates(XDriver driver, XStateType stateType)
		{
			if (driver.AvailableStates.Count == 0)
			{
				driver.AvailableStates.Add(XStateType.Norm);
				driver.AvailableStates.Add(XStateType.Failure);
				driver.AvailableStates.Add(XStateType.Ignore);
			}
			driver.AvailableStates.Add(stateType);
		}

		public static void AddControlAvailableStates(XDriver driver)
		{
			driver.AvailableStates.Add(XStateType.On);
			driver.AvailableStates.Add(XStateType.Off);
			driver.AvailableStates.Add(XStateType.TurningOn);
			driver.AvailableStates.Add(XStateType.TurnOff);
		}
	}
}