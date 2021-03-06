﻿using System;
using System.Collections.Generic;
using System.Linq;
using FiresecAPI.Models;
using FiresecClient;
using Infrastructure;
using Infrastructure.Common;
using Infrastructure.Common.Windows.ViewModels;
using Infrastructure.Events;
using Infrustructure.Plans.Painters;

namespace PlansModule.ViewModels
{
	public class ElementZoneViewModel : BaseViewModel
	{
		public ElementZoneView ElementZoneView { get; private set; }
		public int? ZoneNo { get; private set; }
		public Zone Zone { get; private set; }
		public ZoneState ZoneState { get; private set; }
		List<Guid> deviceUIDs;
		List<DeviceState> deviceStates;

		public ElementZoneViewModel(ElementPolygonZone elementPolygonZone)
		{
			ShowInTreeCommand = new RelayCommand(OnShowInTree);
			DisableAllCommand = new RelayCommand(OnDisableAll, CanDisableAll);
			EnableAllCommand = new RelayCommand(OnEnableAll, CanEnableAll);
			SetGuardCommand = new RelayCommand(OnSetGuard, CanSetGuard);
			UnSetGuardCommand = new RelayCommand(OnUnSetGuard, CanUnSetGuard);

			ZoneNo = elementPolygonZone.ZoneNo;
			Zone = FiresecManager.Zones.FirstOrDefault(x => x.No == ZoneNo);
			if (Zone != null)
			{
				ZoneState = FiresecManager.DeviceStates.ZoneStates.FirstOrDefault(x => x.No == ZoneNo);
				if (ZoneState != null)
				{
					ZoneState.StateChanged += new Action(ZoneState_StateChanged);
				}
			}

			ElementZoneView = new ElementZoneView();
			if (elementPolygonZone.Points == null)
				elementPolygonZone.Points = new System.Windows.Media.PointCollection();
			ElementZoneView._polygon.Points = PainterHelper.GetPoints(elementPolygonZone);
			InitializeDevices();
		}

		void ZoneState_StateChanged()
		{
			OnPropertyChanged("ZoneState");
			OnPropertyChanged("Tooltip");
		}

		public string PresentationName
		{
			get { return "Зона " + Zone.No + "." + Zone.Name; }
		}

		public string Tooltip
		{
			get
			{
				var toolTip = Zone.PresentationName;
				toolTip += "\n" + "Состояние: " + EnumsConverter.StateTypeToClassName(ZoneState.StateType);
				if (Zone.ZoneType == ZoneType.Fire)
				{
					toolTip += "\n" + "Количество датчиков для сработки: " + Zone.DetectorCount.ToString();
				}
				if (Zone.ZoneType == ZoneType.Guard)
				{
					if (FiresecManager.IsZoneOnGuardAlarm(ZoneState))
						toolTip += "\n" + "Охранная тревога";
					else
					{
						if (FiresecManager.IsZoneOnGuard(ZoneState))
							toolTip += "\n" + "На охране";
						else
							toolTip += "\n" + "Не на охране";
					}
				}
				return toolTip;
			}
		}

		bool _isSelected;
		public bool IsSelected
		{
			get { return _isSelected; }
			set
			{
				_isSelected = value;
				ElementZoneView._polygon.StrokeThickness = value ? 1 : 0;
				OnPropertyChanged("IsSelected");

				if (value)
					ElementZoneView.Flush();
			}
		}

		void InitializeDevices()
		{
			deviceUIDs = new List<Guid>();
			deviceStates = new List<DeviceState>();
			foreach (var deviceState in FiresecManager.DeviceStates.DeviceStates)
			{
				if ((deviceState.Device != null) && (deviceState.Device.Driver != null))
				{
					if ((deviceState.Device.ZoneNo == ZoneNo) && (deviceState.Device.Driver.CanDisable))
					{
						deviceUIDs.Add(deviceState.UID);
						deviceStates.Add(deviceState);
					}
				}
			}
		}

		public RelayCommand ShowInTreeCommand { get; private set; }
		void OnShowInTree()
		{
			ServiceFactory.Events.GetEvent<ShowZoneEvent>().Publish(ZoneNo);
		}

		public RelayCommand DisableAllCommand { get; private set; }
		void OnDisableAll()
		{
			if (ServiceFactory.SecurityService.Validate())
				FiresecManager.FiresecService.AddToIgnoreList(deviceUIDs);
		}
		bool CanDisableAll()
		{
			if (Zone.ZoneType == ZoneType.Guard)
				return false;
			return (FiresecManager.CurrentUser.Permissions.Any(x => x == PermissionType.Oper_RemoveFromIgnoreList) && deviceStates.Any(x => !x.IsDisabled));
		}

		public RelayCommand EnableAllCommand { get; private set; }
		void OnEnableAll()
		{
			if (ServiceFactory.SecurityService.Validate())
				FiresecManager.FiresecService.RemoveFromIgnoreList(deviceUIDs);
		}
		bool CanEnableAll()
		{
			if (Zone.ZoneType == ZoneType.Guard)
				return false;
			return (FiresecManager.CurrentUser.Permissions.Any(x => x == PermissionType.Oper_AddToIgnoreList) && deviceStates.Any(x => x.IsDisabled));
		}

		public RelayCommand SetGuardCommand { get; private set; }
		void OnSetGuard()
		{
			if (ServiceFactory.SecurityService.Validate())
				FiresecManager.SetZoneGuard(Zone);
		}
		bool CanSetGuard()
		{
			return ((Zone.ZoneType == ZoneType.Guard) && (Zone.SecPanelUID != null) && (FiresecManager.IsZoneOnGuard(ZoneState) == false));
		}

		public RelayCommand UnSetGuardCommand { get; private set; }
		void OnUnSetGuard()
		{
			if (ServiceFactory.SecurityService.Validate())
				FiresecManager.UnSetZoneGuard(Zone);
		}
		bool CanUnSetGuard()
		{
			return ((Zone.ZoneType == ZoneType.Guard) && (Zone.SecPanelUID != null) && (FiresecManager.IsZoneOnGuard(ZoneState) == true));
		}
	}
}