using BudgetTracker.Helpers;
using BudgetTracker.Models;
using BudgetTracker.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmds.DBus.Protocol;

namespace BudgetTracker.ViewModels
{
	public partial class SettingsPageViewModel : ViewModelBase
	{
		private BudgetContext _context;
		private CategoryService _categoryService;
		[ObservableProperty]
		private string[] _themes = new string[] { "Fluent", "Classic", "Simple" };
		[ObservableProperty]
		private string _selectedTheme = SettingsHelper.DefaultTheme;
		[ObservableProperty]
		public ObservableCollection<CategoryViewModel> _categories;

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
		public async Task AddCategory()
		{
			var category = new Category();
			var categoryVm = new CategoryViewModel(category, _categoryService);
			Categories.Add(categoryVm);
			await _categoryService.AddAsync(category);
		}

		[RelayCommand]
		public async Task DeleteCategory(CategoryViewModel category)
		{
			Categories.Remove(category);
			await _categoryService.DeleteCategory(category.Model);
		}
		public SettingsPageViewModel()
		{
			_selectedTheme = "Fluent";
			Categories = new ObservableCollection<CategoryViewModel>{
				new CategoryViewModel
				{
					Name = "Default",
					Icon = "fa-solid fa-circle",
					ColorCode = 0xFF0000FF // Default color (blue)
				}
			};
			// Do not use _categoryService or _context in this constructor
		}
		public SettingsPageViewModel(BudgetContext context, CategoryService categoryService)
		{
			_context = context;
			_categoryService = categoryService;
			Categories = new ObservableCollection<CategoryViewModel>(
				_categoryService
					.GetAll()
					.Select(cat => CategoryViewModel.FromModel(cat, _categoryService))
			);
		}
	}
}
