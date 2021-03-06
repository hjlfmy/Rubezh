﻿using FiresecAPI;
using FiresecAPI.Models;
using Infrastructure.Common.Windows.ViewModels;

namespace SecurityModule.ViewModels
{
	public class PermissionViewModel : BaseViewModel
	{
		public PermissionViewModel(PermissionType permissionType)
		{
			PermissionType = permissionType;
			PermissionTypeName = permissionType.ToDescription();
		}

		public PermissionType PermissionType { get; private set; }

		public string PermissionTypeName { get; private set; }

		bool _isEnable;
		public bool IsEnable
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