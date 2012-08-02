﻿using System;
using System.Collections.Generic;
using FiresecClient;
using XFiresecAPI;

namespace Commom.GK
{
	public class BinaryObjectBase
	{
		public List<byte> DeviceType { get; protected set; }
		public List<byte> Address { get; protected set; }
		public List<byte> Description { get; protected set; }
		public List<byte> Offset { get; protected set; }
		public List<byte> InputDependensesCount { get; private set; }
		public List<byte> InputDependenses { get; protected set; }
		public List<byte> OutputDependensesCount { get; private set; }
		public List<byte> OutputDependenses { get; protected set; }
		public List<byte> Formula { get; protected set; }
		public List<byte> ParametersCount { get; private set; }
		public List<byte> Parameters { get; protected set; }
		public List<byte> AllBytes { get; private set; }
		public List<FormulaOperation> FormulaOperations { get; protected set; }

		public DatabaseType DatabaseType { get; protected set; }
		public XZone Zone { get; protected set; }
		public XDevice Device { get; protected set; }

		public BinaryObjectBase()
		{
			Description = new List<byte>();
		}

		XBinaryBase GetBinaryBase()
		{
			XBinaryBase binaryBase = null;
			if (Zone != null)
				binaryBase = Zone;
			if (Device != null)
				binaryBase = Device;
			return binaryBase;
		}

		protected void SetAddress(short address)
		{
			Address = new List<byte>();

			if (DatabaseType == DatabaseType.Gk)
			{
				var binaryBase = GetBinaryBase();

				short controllerAddress = 0;
				short no = 0;
				if (binaryBase.KauDatabaseParent != null)
				{
					short lineNo = XManager.GetKauLine(binaryBase.KauDatabaseParent);
					byte intAddress = binaryBase.KauDatabaseParent.IntAddress;
					controllerAddress = (short)(lineNo * 256 + intAddress);
					no = binaryBase.GetDatabaseNo(DatabaseType.Kau);
				}
				else
				{
					controllerAddress = 0x200;
					no = binaryBase.GetDatabaseNo(DatabaseType.Gk);
				}
				Address.AddRange(BytesHelper.ShortToBytes(controllerAddress));

				Address.AddRange(BytesHelper.ShortToBytes(no));
			}
			Address.AddRange(BytesHelper.ShortToBytes(address));
		}

		void InitializeInputOutputDependences()
		{
			var binaryBase = GetBinaryBase();

			InputDependenses = new List<byte>();
			OutputDependenses = new List<byte>();

			foreach (var device in binaryBase.InputDevices)
			{
				var no = device.GetDatabaseNo(DatabaseType);
				InputDependenses.AddRange(BitConverter.GetBytes(no));
			}
			foreach (var device in binaryBase.OutputDevices)
			{
				var no = device.GetDatabaseNo(DatabaseType);
				OutputDependenses.AddRange(BitConverter.GetBytes(no));
			}
			foreach (var zone in binaryBase.InputZones)
			{
				var no = zone.GetDatabaseNo(DatabaseType);
				InputDependenses.AddRange(BitConverter.GetBytes(no));
			}
			foreach (var zone in binaryBase.OutputZones)
			{
				var no = zone.GetDatabaseNo(DatabaseType);
				OutputDependenses.AddRange(BitConverter.GetBytes(no));
			}
		}

		public void InitializeAllBytes()
		{
			switch (DatabaseType)
			{
				case DatabaseType.Gk:
					if (Device != null)
					{
						Description = BytesHelper.StringDescriptionToBytes(Device.ShortPresentationAddressAndDriver);
					}
					if (Zone != null)
					{
						Description = BytesHelper.StringDescriptionToBytes("Зона " + Zone.No.ToString() + " - " + Zone.Name);
					}
					break;

				case DatabaseType.Kau:
					Description = new List<byte>();
					break;
			}

			InitializeInputOutputDependences();

			InputDependensesCount = BytesHelper.ShortToBytes((short)(InputDependenses.Count / 2));
			OutputDependensesCount = BytesHelper.ShortToBytes((short)(OutputDependenses.Count / 2));
			ParametersCount = BytesHelper.ShortToBytes((short)(Parameters.Count / 4));

			int offsetToParameters = 0;
			switch(DatabaseType)
			{
				case DatabaseType.Gk:
					offsetToParameters = 2 + 6 + 32 + 2 + 2 + InputDependenses.Count + 2 + OutputDependenses.Count + Formula.Count;
					break;

				case DatabaseType.Kau:
					offsetToParameters = 2 + 2 + 2 + 2 + OutputDependenses.Count + Formula.Count;
					break;
			}
			Offset = BytesHelper.ShortToBytes((short)offsetToParameters);

			AllBytes = new List<byte>();
			AllBytes.AddRange(DeviceType);
			AllBytes.AddRange(Address);
			AllBytes.AddRange(Description);
			AllBytes.AddRange(Offset);
			if (DatabaseType == DatabaseType.Gk)
			{
				AllBytes.AddRange(InputDependensesCount);
				AllBytes.AddRange(InputDependenses);
			}
			AllBytes.AddRange(OutputDependensesCount);
			AllBytes.AddRange(OutputDependenses);
			AllBytes.AddRange(Formula);
			AllBytes.AddRange(ParametersCount);
			AllBytes.AddRange(Parameters);
		}

		public short GetNo()
		{
			if (Zone != null)
				return Zone.GetDatabaseNo(DatabaseType);
			else
				return Device.GetDatabaseNo(DatabaseType);
		}
	}
}