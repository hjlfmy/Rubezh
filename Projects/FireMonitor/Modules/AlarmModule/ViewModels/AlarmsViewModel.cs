﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AlarmModule.Events;
using FiresecAPI.Models;
using FiresecClient;
using Infrastructure;
using Infrastructure.Common;
using Infrastructure.Common.Windows.ViewModels;

namespace AlarmModule.ViewModels
{
	public class AlarmsViewModel : ViewPartViewModel
	{
		List<Alarm> allAlarms;
		AlarmType? _alarmType;

		public AlarmsViewModel()
		{
			ResetAllCommand = new RelayCommand(OnResetAll);
			RemoveAllFromIgnoreListCommand = new RelayCommand(OnRemoveAllFromIgnoreList, CanRemoveAllFromIgnoreList);
			ServiceFactory.Events.GetEvent<AlarmRemovedEvent>().Subscribe(OnAlarmRemoved);
			ServiceFactory.Events.GetEvent<AlarmAddedEvent>().Subscribe(OnAlarmAdded);

			allAlarms = new List<Alarm>();
			Alarms = new ObservableCollection<AlarmViewModel>();
		}

		public void Initialize()
		{
		}

		public void Sort(AlarmType? alarmType)
		{
			_alarmType = alarmType;

			Alarms.Clear();

			foreach (var alarm in allAlarms)
			{
				if ((alarmType == null) || (alarm.AlarmType == alarmType))
				{
					var alarmViewModel = new AlarmViewModel(alarm);
					Alarms.Add(alarmViewModel);
				}
			}
		}

		public ObservableCollection<AlarmViewModel> Alarms { get; private set; }

		AlarmViewModel _selectedAlarm;
		public AlarmViewModel SelectedAlarm
		{
			get { return _selectedAlarm; }
			set
			{
				_selectedAlarm = value;
				OnPropertyChanged("SelectedAlarm");
			}
		}

		bool alrmWasReset;
		public bool HasAlarmsToReset
		{
			get
			{
				if (alrmWasReset)
					return false;
				return Alarms.Any(x => x.Alarm.AlarmType != AlarmType.Off);
			}
		}

		public RelayCommand ResetAllCommand { get; private set; }
		void OnResetAll()
		{
			var resetItems = new List<ResetItem>();
			foreach (var alarm in allAlarms)
			{
				var resetItem = alarm.GetResetItem();
				if (resetItem != null)
				{
					var existringResetItem = resetItems.FirstOrDefault(x => x.DeviceUID == resetItem.DeviceUID);
					if (existringResetItem != null)
					{
						foreach (string stateName in resetItem.StateNames)
						{
							if (existringResetItem.StateNames.Contains(stateName) == false)
								existringResetItem.StateNames.Add(stateName);
						}
					}
					else
					{
						resetItems.Add(resetItem);
					}
				}
			}

			alrmWasReset = true;
			OnPropertyChanged("HasAlarmsToReset");
			FiresecManager.FiresecService.ResetStates(resetItems);
		}

		public bool CanRemoveAllFromIgnoreList()
		{
			return Alarms.Any(x => x.Alarm.AlarmType == AlarmType.Off);
		}

		public RelayCommand RemoveAllFromIgnoreListCommand { get; private set; }
		void OnRemoveAllFromIgnoreList()
		{
			var deviceUIDs = new List<Guid>();
			foreach (var alarmViewModel in Alarms)
			{
				if (alarmViewModel.Alarm.AlarmType == AlarmType.Off)
				{
					var deviceUID = alarmViewModel.Alarm.DeviceUID;
					var deviceState = FiresecManager.DeviceStates.DeviceStates.FirstOrDefault(x => x.UID == deviceUID);
					if (FiresecManager.CanDisable(deviceState) && deviceState.IsDisabled)

						deviceUIDs.Add(deviceUID);
				}
			}

			if (ServiceFactory.SecurityService.Validate())
			{
				FiresecManager.FiresecService.RemoveFromIgnoreList(deviceUIDs);
			}
		}

		void OnAlarmAdded(Alarm alarm)
		{
			allAlarms.Add(alarm);

			if (_alarmType == null || alarm.AlarmType == _alarmType)
			{
				Alarms.Insert(0, new AlarmViewModel(alarm));
				alrmWasReset = false;
				OnPropertyChanged("HasAlarmsToReset");
			}
		}

		void OnAlarmRemoved(Alarm alarm)
		{
			allAlarms.Remove(alarm);

			if (_alarmType == null || alarm.AlarmType == _alarmType)
			{
				Alarms.Remove(Alarms.FirstOrDefault(x => x.Alarm.DeviceUID == alarm.DeviceUID));
				OnPropertyChanged("HasAlarmsToReset");
			}
		}
	}
}