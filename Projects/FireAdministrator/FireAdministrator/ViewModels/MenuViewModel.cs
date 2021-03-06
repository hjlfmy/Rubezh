﻿using System.ComponentModel;
using Infrastructure.Common.Windows.ViewModels;
using System.Windows.Controls;

namespace FireAdministrator.ViewModels
{
	public class MenuViewModel : BaseViewModel
	{
		private BaseViewModel _extendedMenu;
		public BaseViewModel ExtendedMenu
		{
			get { return _extendedMenu; }
			set
			{
				_extendedMenu = value;
				OnPropertyChanged("ExtendedMenu");
			}
		}

		public event CancelEventHandler SetNewConfigEvent;
		public bool SetNewConfig()
		{
			CancelEventArgs e = new CancelEventArgs(false);
			if (SetNewConfigEvent != null)
				SetNewConfigEvent(this, e);
			return e.Cancel;
		}
	}
}
