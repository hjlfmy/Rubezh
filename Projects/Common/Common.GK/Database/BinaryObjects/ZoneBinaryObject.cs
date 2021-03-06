﻿using System.Collections.Generic;
using FiresecAPI.Models;
using XFiresecAPI;

namespace Common.GK
{
	public class ZoneBinaryObject : BinaryObjectBase
	{
		public ZoneBinaryObject(XZone zone, DatabaseType databaseType)
		{
			DatabaseType = databaseType;
			ObjectType = ObjectType.Zone;
			Zone = zone;
			Build();
		}

		public override void Build()
		{
			DeviceType = BytesHelper.ShortToBytes((ushort)0x100);
			SetAddress((ushort)0);
			Parameters = new List<byte>();
			SetFormulaBytes();
			InitializeAllBytes();
		}

		void SetFormulaBytes()
		{
			Formula = new FormulaBuilder();
			if (DatabaseType == DatabaseType.Gk)
			{
				AddGkZoneFormula();
			}
			else
			{
				Formula.Add(FormulaOperationType.END);
			}
			FormulaBytes = Formula.GetBytes();
		}

		void AddDeviceFire1()
		{
			var count = 0;
			foreach (var device in Zone.Devices)
			{
				if (device.Driver.DriverType == XDriverType.HandDetector)
					continue;

				Formula.AddGetBitOff(XStateType.Fire1, device, DatabaseType);

				if (count > 0)
				{
					Formula.Add(FormulaOperationType.ADD);
				}
				count++;
			}
		}
		void AddDeviceFire2()
		{
			var count = 0;
			foreach (var device in Zone.Devices)
			{
				if (device.Driver.DriverType != XDriverType.HandDetector)
					continue;

				if (device.Driver.DriverType == XDriverType.HandDetector)
					Formula.AddGetBitOff(XStateType.Fire2, device, DatabaseType);

				if (count > 0)
				{
					Formula.Add(FormulaOperationType.OR);
				}
				count++;
			}
			Formula.AddGetBit(XStateType.Fire2, Zone, DatabaseType);
			if (count > 0)
			{
				Formula.Add(FormulaOperationType.OR);
			}
		}

		void AddGkZoneFormula()
		{
			AddDeviceFire1();
			AddDeviceFire2();

			Formula.Add(FormulaOperationType.CONST, 0, Zone.Fire2Count, "Количество устройств для формирования Пожар2");
			Formula.Add(FormulaOperationType.MUL);
			Formula.Add(FormulaOperationType.ADD);
			Formula.Add(FormulaOperationType.DUP);
			Formula.Add(FormulaOperationType.CONST, 0, Zone.Fire2Count, "Количество устройств для формирования Пожар2");
			Formula.Add(FormulaOperationType.GE);
			Formula.Add(FormulaOperationType.DUP);
			Formula.AddPutBit(XStateType.Fire2, Zone, DatabaseType);

			Formula.Add(FormulaOperationType.CONST, 0, 0x8000, "15-ый бит");
			Formula.Add(FormulaOperationType.MUL);
			Formula.Add(FormulaOperationType.CONST, 0, 0x8000, "15-ый бит");
			Formula.Add(FormulaOperationType.XOR);
			Formula.Add(FormulaOperationType.OR);
			Formula.Add(FormulaOperationType.DUP);
			Formula.AddGetBit(XStateType.Fire1, Zone, DatabaseType);
			Formula.Add(FormulaOperationType.CONST, 0, Zone.Fire1Count, "Количество устройств для формирования Пожар1");
			Formula.Add(FormulaOperationType.MUL);
			Formula.Add(FormulaOperationType.ADD);
			Formula.Add(FormulaOperationType.CONST, 0, (ushort)(Zone.Fire1Count + 0x8000), "15-ый бит + rоличество устройств для формирования Пожар1");
			Formula.Add(FormulaOperationType.GE);
			Formula.Add(FormulaOperationType.DUP);
			Formula.AddPutBit(XStateType.Fire1, Zone, DatabaseType);

			Formula.Add(FormulaOperationType.CONST, 0, 0x8000, "15-ый бит");
			Formula.Add(FormulaOperationType.MUL);
			Formula.Add(FormulaOperationType.COM);
			Formula.Add(FormulaOperationType.AND);
			Formula.Add(FormulaOperationType.CONST, 0, 0x8001, "15-ый бит + rоличество устройств для формирования Внимание");
			Formula.Add(FormulaOperationType.GE);
			Formula.AddPutBit(XStateType.Attention, Zone, DatabaseType);
			Formula.Add(FormulaOperationType.END);
		}
	}
}