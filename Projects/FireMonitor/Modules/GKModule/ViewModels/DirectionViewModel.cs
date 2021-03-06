﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Common.Windows.ViewModels;
using FiresecAPI;
using XFiresecAPI;
using Infrastructure.Common;
using Common.GK;
using FiresecClient;

namespace GKModule.ViewModels
{
	public class DirectionViewModel : BaseViewModel
	{
		public XDirectionState DirectionState { get; private set; }
		public XDirection Direction
		{
			get { return DirectionState.Direction; }
		}

		public DirectionViewModel(XDirectionState directionState)
		{
			SetIgnoreCommand = new RelayCommand(OnSetIgnore, CanSetIgnore);
			ResetIgnoreCommand = new RelayCommand(OnResetIgnore, CanResetIgnore);
			SetAutomaticCommand = new RelayCommand(OnSetAutomatic);
			ResetAutomaticCommand = new RelayCommand(OnResetAutomatic);
			TurnOnCommand = new RelayCommand(OnTurnOn);
			CancelDelayCommand = new RelayCommand(OnCancelDelay);
			TurnOffCommand = new RelayCommand(OnTurnOff);
			StopCommand = new RelayCommand(OnStop);
			CancelStartCommand = new RelayCommand(OnCancelStart);
			TurnOnNowCommand = new RelayCommand(OnTurnOnNow);
			TurnOffNowCommand = new RelayCommand(OnTurnOffNow);

			DirectionState = directionState;
			DirectionState.StateChanged += new System.Action(OnStateChanged);
			OnStateChanged();
		}

		void OnStateChanged()
		{
			OnPropertyChanged("DirectionState");
			OnPropertyChanged("ToolTip");
		}

		public string ToolTip
		{
			get
			{
				var toolTip = Direction.PresentationName;
				toolTip += "\n" + "Состояние: " + DirectionState.StateType.ToDescription();
				return toolTip;
			}
		}

		public RelayCommand SetIgnoreCommand { get; private set; }
		void OnSetIgnore()
		{
			SendControlCommand(0x86);
		}
		bool CanSetIgnore()
		{
			return !DirectionState.States.Contains(XStateType.Ignore);
		}

		public RelayCommand ResetIgnoreCommand { get; private set; }
		void OnResetIgnore()
		{
			SendControlCommand(0x06);
		}
		bool CanResetIgnore()
		{
			return DirectionState.States.Contains(XStateType.Ignore);
		}

		public RelayCommand SetAutomaticCommand { get; private set; }
		void OnSetAutomatic()
		{
			SendControlCommand(0x80);
		}

		public RelayCommand ResetAutomaticCommand { get; private set; }
		void OnResetAutomatic()
		{
			SendControlCommand(0x00);
		}

		public RelayCommand TurnOnCommand { get; private set; }
		void OnTurnOn()
		{
			SendControlCommand(0x8b);
		}

		public RelayCommand CancelDelayCommand { get; private set; }
		void OnCancelDelay()
		{
			SendControlCommand(0x8c);
		}

		public RelayCommand TurnOffCommand { get; private set; }
		void OnTurnOff()
		{
			SendControlCommand(0x8d);
		}

		public RelayCommand StopCommand { get; private set; }
		void OnStop()
		{
			SendControlCommand(0x8e);
		}

		public RelayCommand CancelStartCommand { get; private set; }
		void OnCancelStart()
		{
			SendControlCommand(0x8f);
		}

		public RelayCommand TurnOnNowCommand { get; private set; }
		void OnTurnOnNow()
		{
			SendControlCommand(0x90);
		}

		public RelayCommand TurnOffNowCommand { get; private set; }
		void OnTurnOffNow()
		{
			SendControlCommand(0x91);
		}

		void SendControlCommand(byte code)
		{
			var bytes = new List<byte>();
			bytes.AddRange(BytesHelper.ShortToBytes(Direction.GetDatabaseNo(DatabaseType.Gk)));
			bytes.Add(code);
			SendManager.Send(Direction.GkDatabaseParent, 3, 13, 0, bytes);
		}
	}
}