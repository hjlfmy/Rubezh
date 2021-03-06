﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Threading;
using FiresecAPI.Models;
using FiresecClient;
using Infrastructure;
using Infrastructure.Common;
using Infrastructure.Common.Windows.ViewModels;

namespace DevicesModule.ViewModels
{
	public class DeviceControlViewModel : BaseViewModel
	{
		Device Device;
		bool IsBuisy = false;

		public DeviceControlViewModel(Device device)
		{
			Device = device;
			ConfirmCommand = new RelayCommand(OnConfirm, CanConfirm);

			Blocks = new List<BlockViewModel>();

			foreach (var property in device.Driver.Properties)
			{
				if (property.IsControl)
				{
					var blockViewModel = Blocks.FirstOrDefault(x => x.Name == property.BlockName);
					if (blockViewModel == null)
					{
						blockViewModel = new BlockViewModel()
						{
							Name = property.BlockName
						};
						Blocks.Add(blockViewModel);
					}
					blockViewModel.Commands.Add(property);
				}
			}
		}

		public List<BlockViewModel> Blocks { get; private set; }

		BlockViewModel _selectedBlock;
		public BlockViewModel SelectedBlock
		{
			get { return _selectedBlock; }
			set
			{
				_selectedBlock = value;
				OnPropertyChanged("SelectedBlock");
			}
		}

		bool CanConfirm()
		{
			return SelectedBlock != null && SelectedBlock.SelectedCommand != null && IsBuisy == false;
		}

		public RelayCommand ConfirmCommand { get; private set; }
		void OnConfirm()
		{
			IsBuisy = true;
			if (ServiceFactory.SecurityService.Validate())
			{
				var thread = new Thread(DoConfirm);
				thread.Start();
			}
		}

		void DoConfirm()
		{
			var result = FiresecManager.FiresecService.ExecuteCommand(Device.UID, GetCommandName());
			Dispatcher.BeginInvoke(new Action(() => { IsBuisy = false; OnPropertyChanged("ConfirmCommand"); }));
		}

		#region Valve
		string GetCommandName()
		{
			var commandName = SelectedBlock.SelectedCommand.Name;
			if (Device.Driver.DriverType == DriverType.Valve)
			{
				if (!HasActionProprty())
				{
					if (commandName == "BoltOpen")
						return "BoltClose";
					if (commandName == "BoltClose")
						return "BoltOpen";
				}
			}
			return commandName;
		}

		bool HasActionProprty()
		{
			var actionProperty = Device.Properties.FirstOrDefault(x => x.Name == "Action");
			if (actionProperty != null)
			{
				return actionProperty.Value == "1";
			}
			return false;
		}
		#endregion
	}

	public class BlockViewModel : BaseViewModel
	{
		public BlockViewModel()
		{
			Commands = new List<DriverProperty>();
		}

		public string Name { get; set; }
		public List<DriverProperty> Commands { get; set; }

		DriverProperty _selectedCommand;
		public DriverProperty SelectedCommand
		{
			get { return _selectedCommand; }
			set
			{
				_selectedCommand = value;
				OnPropertyChanged("SelectedCommand");
			}
		}
	}
}