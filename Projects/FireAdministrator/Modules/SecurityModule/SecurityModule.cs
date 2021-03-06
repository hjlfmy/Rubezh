﻿using System.Collections.Generic;
using Infrastructure;
using Infrastructure.Common;
using Infrastructure.Common.Navigation;
using Infrastructure.Events;
using SecurityModule.ViewModels;
using Infrastructure.Client;

namespace SecurityModule
{
	public class SecurityModule : ModuleBase
	{
		UsersViewModel _usersViewModel;
		RolesViewModel _groupsViewModel;

		public SecurityModule()
		{
			_usersViewModel = new UsersViewModel();
			_groupsViewModel = new RolesViewModel();
		}

		public override void Initialize()
		{
			_usersViewModel.Initialize();
			_groupsViewModel.Initialize();
		}
		public override IEnumerable<NavigationItem> CreateNavigation()
		{
			return new List<NavigationItem>()
			{
				new NavigationItem("Права доступа", null, new List<NavigationItem>(){
					new NavigationItem<ShowUsersEvent>(_usersViewModel, "Пользователи", "/Controls;component/Images/user.png"),
					new NavigationItem<ShowUserGroupsEvent>(_groupsViewModel, "Группы", "/Controls;component/Images/users.png"),
				}),
			};
		}
		public override string Name
		{
			get { return "Права доступа"; }
		}
	}
}