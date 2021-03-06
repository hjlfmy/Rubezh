﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Common.Windows.ViewModels;
using System.Windows.Input;

namespace Controls.Menu.ViewModels
{
	public class MenuButtonViewModel : BaseViewModel
	{
		public MenuButtonViewModel(ICommand command = null, string imageSource = null, string toolTip = null)
		{
			Command = command;
			ImageSource = imageSource;
			ToolTip = toolTip;
		}

		private string _toolTip;
		public string ToolTip
		{
			get { return _toolTip; }
			set
			{
				_toolTip = value;
				OnPropertyChanged("ToolTip");
			}
		}

		private string _imageSource;
		public string ImageSource
		{
			get { return _imageSource; }
			set
			{
				_imageSource = value;
				OnPropertyChanged("ImageSource");
			}
		}

		private ICommand _command;
		public ICommand Command
		{
			get { return _command; }
			set
			{
				_command = value;
				OnPropertyChanged("Command");
			}
		}
	}
}
