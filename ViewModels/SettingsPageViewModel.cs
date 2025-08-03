using BudgetTracker.Helpers;
using BudgetTracker.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker.ViewModels
{
	public partial class SettingsPageViewModel : ViewModelBase
	{
		private BudgetContext _context;
		[ObservableProperty]
		private string[] _themes = new string[] { "Fluent", "Classic", "Simple" };
		[ObservableProperty]
		private string _selectedTheme = SettingsHelper.DefaultTheme;

		public ObservableCollection<Category> Categories { get; set; }
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
		[RelayCommand]
		public void AddNewCategory()
		{
			Categories.Add(new Category());
		}
		public SettingsPageViewModel()
		{
			//Dumb data for designer preview
			Categories = new ObservableCollection<Category>
			{
				new Category { Id = 1, Name = "Food", Icon = "\ue900", ColorCode = 0xFFFF0000 },
				new Category { Id = 2, Name = "Transport", Icon = "\ue901", ColorCode = 0xFF8A2BE2 },
				new Category { Id = 3, Name = "Entertainment", Icon = "\ue902", ColorCode = 0xFF0000FF },
				new Category { Id = 4, Name = "Utilities", Icon = "\ue903", ColorCode = 0xFFADFF2F }
			};
		}
		public SettingsPageViewModel(BudgetContext context)
		{
			_context = context;
			Categories = new ObservableCollection<Category>(context.Categories.ToList());
		}
	}
}
