﻿using FiresecAPI.Models;
using Infrastructure.Common.Windows.ViewModels;
using FiresecAPI;

namespace JournalModule.ViewModels
{
	public class ClassViewModel : BaseViewModel
	{
		public ClassViewModel(StateType stateType)
		{
			StateType = stateType;
		}

		public StateType StateType { get; private set; }

		bool? _isEnable = false;
		public bool? IsEnable
		{
			get { return _isEnable; }
			set
			{
				_isEnable = value;
				OnPropertyChanged("IsEnable");
			}
		}
	}
}