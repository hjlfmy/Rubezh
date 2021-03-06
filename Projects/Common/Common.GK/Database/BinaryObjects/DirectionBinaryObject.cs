﻿using System;
using System.Collections.Generic;
using XFiresecAPI;

namespace Common.GK
{
	public class DirectionBinaryObject : BinaryObjectBase
	{
		public DirectionBinaryObject(XDirection direction, DatabaseType databaseType)
		{
			DatabaseType = databaseType;
			ObjectType = ObjectType.Direction;
			Direction = direction;
			Build();
		}

		public override void Build()
		{
			DeviceType = BytesHelper.ShortToBytes((ushort)0x106);
			SetAddress((ushort)0);
			Parameters = new List<byte>();
			SetFormulaBytes();
			SetPropertiesBytes();
			InitializeAllBytes();
		}

		void SetFormulaBytes()
		{
			Formula = new FormulaBuilder();
			if ((Direction.Zones.Count > 0) && (Direction.DirectionDevices.Count > 0))
				AddFormula();
			else
				Formula.Add(FormulaOperationType.END);
			FormulaBytes = Formula.GetBytes();
		}

		void AddFormula()
		{
			var zonesCount = 0;
			foreach (var zone in Direction.XZones)
			{
				Formula.AddGetBitOff(XStateType.Fire2, zone, DatabaseType);
				if (zonesCount > 0)
				{
					Formula.Add(FormulaOperationType.ADD);
				}
				zonesCount++;
			}
			Formula.AddGetBit(XStateType.Norm, Direction, DatabaseType);
			Formula.Add(FormulaOperationType.AND, comment: "Смешивание с битом Дежурный Направления");
			Formula.AddPutBit(XStateType.TurnOn, Direction, DatabaseType);

			Formula.Add(FormulaOperationType.END);
		}

		void SetPropertiesBytes()
		{
			var binProperties = new List<BinProperty>();
			binProperties.Add(new BinProperty()
			{
				No = 0,
				Value = Direction.Delay
			});
			binProperties.Add(new BinProperty()
			{
				No = 1,
				Value = Direction.Hold
			});
			binProperties.Add(new BinProperty()
			{
				No = 2,
				Value = Direction.Regime
			});

			Parameters = new List<byte>();
			foreach (var binProperty in binProperties)
			{
				Parameters.Add(binProperty.No);
				Parameters.AddRange(BitConverter.GetBytes(binProperty.Value));
				Parameters.Add(0);
			}
		}
	}
}