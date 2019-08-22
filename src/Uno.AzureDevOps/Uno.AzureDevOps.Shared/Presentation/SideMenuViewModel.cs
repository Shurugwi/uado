﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Uno.AzureDevOps.Business;
using Uno.AzureDevOps.Business.Extensions;
using Uno.AzureDevOps.Business.VSTS;
using Uno.AzureDevOps.Client;
using Uno.AzureDevOps.Framework.Navigation;
using Uno.AzureDevOps.Framework.Tasks;

namespace Uno.AzureDevOps.Presentation
{
	[Windows.UI.Xaml.Data.Bindable]
	public class SideMenuViewModel : ViewModelBase
	{
		private readonly IStackNavigationService _navigationService;
		private readonly IUserPreferencesService _userPreferencesService;
		private readonly IVSTSRepository _vstsRespository;
		private ITaskNotifier<UserProfile> _userProfile;
		private AccountData _account;

		public SideMenuViewModel()
		{
			_navigationService = SimpleIoc.Default.GetInstance<IStackNavigationService>();
			_userPreferencesService = SimpleIoc.Default.GetInstance<IUserPreferencesService>();
			_vstsRespository = SimpleIoc.Default.GetInstance<IVSTSRepository>();

			UserProfile = new TaskNotifier<UserProfile>(_vstsRespository.GetUserProfile());
			_account = _userPreferencesService.GetPreferredAccount();

			ToProfilePage = new RelayCommand(() => _navigationService.ToProfilePage());
			ToProjectListPage = new RelayCommand(() => _navigationService.ToProjectListPage(_account));
			ToOrganizationListPage = new RelayCommand(() => _navigationService.ToOrganizationListPage());
		}

		public ITaskNotifier<UserProfile> UserProfile
		{
			get => _userProfile;
			set => Set(() => UserProfile, ref _userProfile, value);
		}

		public ICommand ToProfilePage { get; }

		public ICommand ToOrganizationListPage { get; }

		public ICommand ToProjectListPage { get; }
	}
}