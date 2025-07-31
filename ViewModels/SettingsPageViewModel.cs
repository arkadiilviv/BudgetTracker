using BudgetTracker.Models;
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
		[RelayCommand]
		public void ToggleTheme()
		{
			SettingsHelper.ToggleTheme();
			Process.Start(Environment.ProcessPath!);
			Environment.Exit(0);
		}
	}
}
