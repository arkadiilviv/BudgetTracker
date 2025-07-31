using BudgetTracker.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker.ViewModels
{
	public partial class SettingsPageViewModel : ViewModelBase
	{
		[ObservableProperty]
		private string[] _themes = new string[] {"Fluent","Classic", "Simple"};
		[ObservableProperty]
		private string _selectedTheme = SettingsHelper.DefaultTheme;
		[RelayCommand]
		public void SetTheme()
		{
			SettingsHelper.SetTheme(_selectedTheme);
			Process.Start(Environment.ProcessPath!);
			Environment.Exit(0);
		}
		[RelayCommand]
		public void SetDefaultTheme()
		{
			SelectedTheme = "Fluent";
		}
	}
}
