﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FiresecAPI.Models;
using FiresecService.Converters;

namespace FiresecService
{
    public class Watcher
    {
        internal void Start()
        {
            FiresecInternalClient.NewEvent += new Action<int>(FiresecClient_NewEvent);
            OnStateChanged();
            OnParametersChanged();
            SetLastEvent();
        }

        void FiresecClient_NewEvent(int EventMask)
        {
            bool evmNewEvents = ((EventMask & 1) == 1);
            bool evmStateChanged = ((EventMask & 2) == 2);
            bool evmConfigChanged = ((EventMask & 4) == 4);
            bool evmDeviceParamsUpdated = ((EventMask & 8) == 8);
            bool evmPong = ((EventMask & 16) == 16);
            bool evmDatabaseChanged = ((EventMask & 32) == 32);
            bool evmReportsChanged = ((EventMask & 64) == 64);
            bool evmSoundsChanged = ((EventMask & 128) == 128);
            bool evmLibraryChanged = ((EventMask & 256) == 256);
            bool evmPing = ((EventMask & 512) == 512);
            bool evmIgnoreListChanged = ((EventMask & 1024) == 1024);
            bool evmEventViewChanged = ((EventMask & 2048) == 2048);

            if (evmStateChanged)
            {
                OnStateChanged();
            }
            if (evmDeviceParamsUpdated)
            {
                OnParametersChanged();
            }
            if (evmNewEvents)
            {
                OnNewEvent();
            }
        }

        int LastEventId = 0;

        void SetLastEvent()
        {
            Firesec.ReadEvents.document journal = FiresecInternalClient.ReadEvents(0, 1);
            if ((journal.Journal != null) && (journal.Journal.Count() > 0))
            {
                LastEventId = Convert.ToInt32(journal.Journal[0].IDEvents);
            }
        }

        // ДОБАВИТЬ ПРОВЕРКУ - ЕСЛИ В ВЫЧИТАННЫХ 100 СОБЫТИЯХ ВСЕ СОБЫТИЯ НОВЫЕ, ТО ВЫЧИТАТЬ И ВТОРУЮ СОТНЮ

        void OnNewEvent()
        {
            var document = FiresecInternalClient.ReadEvents(0, 100);

            if ((document != null) && (document.Journal.Count() > 0))
            {
                foreach (var innerJournalItem in document.Journal)
                {
                    if (Convert.ToInt32(innerJournalItem.IDEvents) > LastEventId)
                    {
                        var journalItem = JournalConverter.Convert(innerJournalItem);
                        CallbackManager.OnNewJournalItem(journalItem);
                    }
                }
                LastEventId = Convert.ToInt32(document.Journal[0].IDEvents);
            }
        }

        void OnParametersChanged()
        {
            var coreParameters = FiresecInternalClient.GetDeviceParams();
            try
            {
                foreach (var deviceState in FiresecManager.DeviceConfigurationStates.DeviceStates)
                {
                    deviceState.ChangeEntities.Reset();

                    var innerDevice = coreParameters.dev.FirstOrDefault(x => x.name == deviceState.PlaceInTree);
                    if (innerDevice != null)
                    {
                        foreach (var parameter in deviceState.Parameters)
                        {
                            if ((innerDevice.dev_param != null) && (innerDevice.dev_param.Any(x => x.name == parameter.Name)))
                            {
                                var innerParameter = innerDevice.dev_param.FirstOrDefault(x => x.name == parameter.Name);
                                if (parameter.Value != innerParameter.value)
                                {
                                    deviceState.ChangeEntities.ParameterChanged = true;
                                    if (parameter.Visible)
                                        deviceState.ChangeEntities.VisibleParameterChanged = true;
                                }
                                parameter.Value = innerParameter.value;
                            }
                        }

                        if (deviceState.ChangeEntities.ParameterChanged)
                        {
                            CallbackManager.OnDeviceParametersChanged(deviceState);
                        }
                    }
                }
            }
            catch (Exception e)
            {
            }
        }

        public void OnStateChanged()
        {
            var coreState = FiresecInternalClient.GetCoreState();
            try
            {
                SetStates(coreState);
                PropogateStates();
                CalculateStates();
                CalculateZones();

                foreach (var deviceState in FiresecManager.DeviceConfigurationStates.DeviceStates)
                {
                    if ((deviceState.ChangeEntities.StatesChanged) || (deviceState.ChangeEntities.StateChanged))
                    {
                        deviceState.OnStateChanged();
                        CallbackManager.OnDeviceStateChanged(deviceState);
                    }
                }
            }
            catch (Exception e)
            {
                ;
            }
        }

        void SetStates(Firesec.CoreState.config coreState)
        {
            foreach (var deviceState in FiresecManager.DeviceConfigurationStates.DeviceStates)
            {
                Firesec.CoreState.devType innerDevice = FindDevice(coreState.dev, deviceState.PlaceInTree);

                bool hasOneActiveState = false;

                if (innerDevice != null)
                {
                    foreach (var state in deviceState.InnerStates)
                    {
                        bool IsActive = innerDevice.state.Any(a => a.id == state.Id);
                        if (state.IsActive != IsActive)
                        {
                            hasOneActiveState = true;
                        }
                        state.IsActive = IsActive;
                    }
                }
                else
                {
                    foreach (var state in deviceState.InnerStates)
                    {
                        if (state.IsActive)
                        {
                            hasOneActiveState = true;
                        }
                        state.IsActive = false;
                    }
                }

                if (hasOneActiveState)
                {
                    deviceState.ChangeEntities.StatesChanged = true;
                }
                else
                {
                    deviceState.ChangeEntities.StatesChanged = false;
                }
            }
        }

        void PropogateStates()
        {
            foreach (var deviceState in FiresecManager.DeviceConfigurationStates.DeviceStates)
            {
                deviceState.ParentInnerStates = new List<InnerState>();
                deviceState.ParentStringStates = new List<string>();
            }

            foreach (var deviceState in FiresecManager.DeviceConfigurationStates.DeviceStates)
            {
                foreach (var state in deviceState.InnerStates)
                {
                    if ((state.IsActive) && (state.AffectChildren))
                    {
                        foreach (var chilDevice in FiresecManager.DeviceConfigurationStates.DeviceStates)
                        {
                            if ((chilDevice.PlaceInTree.StartsWith(deviceState.PlaceInTree)) && (chilDevice.PlaceInTree != deviceState.PlaceInTree))
                            {
                                chilDevice.ParentInnerStates.Add(state);
                                var device = FiresecManager.DeviceConfiguration.Devices.FirstOrDefault(x => x.Id == deviceState.Id);
                                chilDevice.ParentStringStates.Add(device.Driver.ShortName + " - " + state.Name);
                                chilDevice.ChangeEntities.StatesChanged = true;
                            }
                        }
                    }
                }
            }
        }

        void CalculateStates()
        {
            foreach (var deviceState in FiresecManager.DeviceConfigurationStates.DeviceStates)
            {
                int minPriority = 7;
                InnerState sourceState = null;

                foreach (var state in deviceState.InnerStates)
                {
                    if (state.IsActive)
                    {
                        if (minPriority > state.Priority)
                            minPriority = state.Priority;
                    }
                }
                foreach (var state in deviceState.ParentInnerStates)
                {
                    if (state.IsActive)
                    {
                        if (minPriority > state.Priority)
                        {
                            minPriority = state.Priority;
                            sourceState = state;
                        }
                    }
                }
                if (deviceState.MinPriority != minPriority)
                {
                    deviceState.ChangeEntities.StateChanged = true;
                }
                else
                {
                    deviceState.ChangeEntities.StateChanged = false;
                }
                deviceState.State = new State() { Id = minPriority };
                deviceState.MinPriority = minPriority;

                if (sourceState != null)
                {
                    deviceState.SourceState = sourceState.Name;
                }
                else
                {
                    deviceState.SourceState = "";
                }
            }
        }

        void CalculateZones()
        {
            if (FiresecManager.DeviceConfigurationStates.ZoneStates != null)
            {
                foreach (var zoneState in FiresecManager.DeviceConfigurationStates.ZoneStates)
                {
                    int minZonePriority = 8;
                    foreach (var device in FiresecManager.DeviceConfiguration.Devices)
                    {
                        if (device.ZoneNo == zoneState.No)
                        {
                            var deviceState = FiresecManager.DeviceConfigurationStates.DeviceStates.FirstOrDefault(x => x.Id == device.Id);
                            // добавить проверку - нужно ли включать устройство при формировании состояния зоны
                            if (deviceState.MinPriority < minZonePriority)
                                minZonePriority = deviceState.MinPriority;
                        }
                    }

                    State newZoneState = new State() { Id = minZonePriority };

                    bool ZoneChanged = false;

                    if (zoneState.State.Id != newZoneState.Id)
                    {
                        ZoneChanged = true;
                    }
                    zoneState.State = newZoneState;

                    if (ZoneChanged)
                    {
                        CallbackManager.OnZoneStateChanged(zoneState);
                    }
                }
            }
        }

        Firesec.CoreState.devType FindDevice(Firesec.CoreState.devType[] innerDevices, string PlaceInTree)
        {
            if (innerDevices != null)
            {
                return innerDevices.FirstOrDefault(a => a.name == PlaceInTree);
            }
            return null;
        }
    }
}