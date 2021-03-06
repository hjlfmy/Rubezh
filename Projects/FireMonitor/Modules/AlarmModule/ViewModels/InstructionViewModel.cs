﻿using System;
using System.Collections.Generic;
using System.Linq;
using FiresecAPI;
using FiresecAPI.Models;
using FiresecClient;
using Infrastructure.Common.Windows.ViewModels;

namespace AlarmModule.ViewModels
{
	public class InstructionViewModel : DialogViewModel
	{
		public bool HasContent { get; private set; }

		public InstructionViewModel(Guid deviceUID, AlarmType alarmType)
		{
			Title = "Инструкции";

			DeviceId = deviceUID;
			StateType = AlarmTypeToStateType(alarmType);

			HasContent = FindDeviceInstruction(DeviceId) || FindZoneInstruction(ZoneNo);
			if (!HasContent)
			{
				Instruction = InstructionGeneral;
			}
		}

		public Guid DeviceId { get; private set; }
		public StateType StateType { get; private set; }
		public Instruction Instruction { get; private set; }

		public int? ZoneNo
		{
			get
			{
				var device = FiresecManager.Devices.FirstOrDefault(x => x.UID == DeviceId);
				if (device != null)
					return device.ZoneNo;
				return null;
			}
		}

		List<Instruction> AvailableStateTypeInstructions
		{
			get
			{
				if (FiresecClient.FiresecManager.SystemConfiguration.Instructions.IsNotNullOrEmpty())
					return FiresecClient.FiresecManager.SystemConfiguration.Instructions.FindAll(x => x.StateType == StateType);
				return new List<Instruction>();
			}
		}

		Instruction InstructionGeneral
		{
			get
			{
				if (AvailableStateTypeInstructions.IsNotNullOrEmpty())
					return AvailableStateTypeInstructions.FirstOrDefault(x => x.InstructionType == InstructionType.General);
				return new Instruction();
			}
		}

		StateType AlarmTypeToStateType(AlarmType alarmType)
		{
			switch (alarmType)
			{
				case AlarmType.Fire:
					return StateType.Fire;

				case AlarmType.Attention:
					return StateType.Attention;

				case AlarmType.Failure:
					return StateType.Failure;

				case AlarmType.Off:
					return StateType.Off;

				case AlarmType.Info:
					return StateType.Info;

				case AlarmType.Service:
					return StateType.Service;

				default:
					return StateType.No;
			}
		}

		bool FindDeviceInstruction(Guid deviceUID)
		{
			if (AvailableStateTypeInstructions.IsNotNullOrEmpty())
			{
				foreach (var instruction in AvailableStateTypeInstructions)
				{
					if (instruction.Devices.IsNotNullOrEmpty() && instruction.Devices.Contains(deviceUID))
					{
						Instruction = instruction;
						return true;
					}
				}
			}

			return false;
		}

		bool FindZoneInstruction(int? zoneNo)
		{
			if (AvailableStateTypeInstructions.IsNotNullOrEmpty())
			{
				foreach (var instruction in AvailableStateTypeInstructions)
				{
					if (zoneNo != null)
						if (instruction.Zones.IsNotNullOrEmpty() && instruction.Zones.Contains(zoneNo.Value))
						{
							Instruction = instruction;
							return true;
						}
				}
			}

			return false;
		}
	}
}