﻿using System.Windows;
using Common;
using FiresecClient;
using Infrastructure;
using Infrastructure.Common.Windows;
using Infrastructure.Common.Windows.ViewModels;

namespace FireAdministrator.ViewModels
{
	public class AdministratorShellViewModel : ShellViewModel
	{
		public AdministratorShellViewModel()
		{
			Title = "Администратор ОПС FireSec-2";
			Height = 700;
			Width = 1100;
			MinWidth = 800;
			MinHeight = 550;
			Toolbar = new MenuViewModel();
		}

		public override bool OnClosing(bool isCanceled)
		{
			AlarmPlayerHelper.Dispose();
			if (ServiceFactory.SaveService.HasChanges)
			{
				var result = MessageBoxService.ShowQuestion("Сохранить изменения в настройках?");
				switch (result)
				{
					case MessageBoxResult.Yes:
						return ((MenuViewModel)Toolbar).SetNewConfig();
					case MessageBoxResult.No:
						return false;
					case MessageBoxResult.Cancel:
						return true;
				}
			}
			return base.OnClosing(isCanceled);
		}
		public override void OnClosed()
		{
			FiresecManager.Disconnect();
			base.OnClosed();
		}
	}
}
