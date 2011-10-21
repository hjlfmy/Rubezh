﻿using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using FiresecAPI.Models;
using FiresecClient;
using Infrastructure.Common;

namespace AlarmModule.ViewModels
{
    public class InstructionViewModel : DialogContent
    {
        public InstructionViewModel(Guid deviceUID, AlarmType alarmType)
        {
            Title = "Инструкции";

            DeviceId = deviceUID;
            StateType = AlarmTypeToStateType(alarmType);
            InicializeInstruction();

            CloseCommand = new RelayCommand(OnCloseInstruction);
        }

        void InicializeInstruction()
        {
            if (!FindDeviceInstruction(DeviceId) && !FindZoneInstruction(ZoneNo))
                Instruction = InstructionGeneral;
        }

        public Guid DeviceId { get; private set; }
        public StateType StateType { get; private set; }
        public Instruction Instruction { get; private set; }

        public ulong? ZoneNo
        {
            get
            {
                var device = FiresecManager.DeviceConfiguration.Devices.FirstOrDefault(x => x.UID == DeviceId);
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
                    return (StateType) 0;

                case AlarmType.Attention:
                    return (StateType) 1;

                case AlarmType.Failure:
                    return (StateType) 2;

                case AlarmType.Off:
                    return (StateType) 4;

                case AlarmType.Info:
                    return (StateType) 6;

                case AlarmType.Service:
                    return (StateType) 3;

                default:
                    return (StateType) 8;
            }
        }

        bool FindDeviceInstruction(Guid deviceUID)
        {
            if (AvailableStateTypeInstructions.IsNotNullOrEmpty())
            {
                foreach (var instruction in AvailableStateTypeInstructions)
                {
                    if (instruction.InstructionDevicesList.IsNotNullOrEmpty() && instruction.InstructionDevicesList.Contains(deviceUID))
                    {
                        Instruction = instruction;
                        return true;
                    }
                }
            }

            return false;
        }

        bool FindZoneInstruction(ulong? zoneNo)
        {
            if (AvailableStateTypeInstructions.IsNotNullOrEmpty())
            {
                foreach (var instruction in AvailableStateTypeInstructions)
                {
                    if (instruction.InstructionZonesList.IsNotNullOrEmpty() && instruction.InstructionZonesList.Contains(zoneNo))
                    {
                        Instruction = instruction;
                        return true;
                    }
                }
            }

            return false;
        }

        public RelayCommand CloseCommand { get; private set; }
        void OnCloseInstruction()
        {
            Close(true);
        }
    }
}