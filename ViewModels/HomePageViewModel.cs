using Avalonia.Media;
using BudgetTracker.Models;
using BudgetTracker.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker.ViewModels
{
	public partial class HomePageViewModel : ViewModelBase
	{
		private CategoryService _categoryService;
		private BudgetContext _context;
		[ObservableProperty]
		private bool _isQuickSettingsVisible = false;
		[ObservableProperty]
		private bool _isBusy = false;
		[ObservableProperty]
		private CategoryViewModel _selectedCatgory = new CategoryViewModel();
		[ObservableProperty]
		private string _inputText = string.Empty;
		[ObservableProperty]
		private decimal _inputCount = 0;
		[ObservableProperty]
		private string _title = "Test";

		[ObservableProperty]
		private DateTimeOffset _selectedDate = new DateTimeOffset(DateTime.Now);
		[ObservableProperty]
		private ObservableCollection<CustomPieSeries<decimal>> _series = new ObservableCollection<CustomPieSeries<decimal>>();

		public ObservableCollection<CategoryViewModel> Categories { get => _categoryService.GetAll(); }

		[RelayCommand]
		public void ToggleQuickSettings()
		{
			IsQuickSettingsVisible = !IsQuickSettingsVisible;
		}
		[RelayCommand]
		public void AddTransaction()
		{
			var selectedSeries = Series.FirstOrDefault(s => s.Name == _inputText && s.Category.Name == SelectedCatgory.Name);
			if (selectedSeries != null)
			{
				if (selectedSeries is CustomPieSeries<decimal> pieSeries)
				{
					selectedSeries.SetValues(new List<decimal> { selectedSeries.Values.First() + _inputCount });
				}
			} else
			{
				Color col = SelectedCatgory.Color;
				Series.Add(new CustomPieSeries<decimal>()
				{
					Name = _inputText,
					Values = new List<Decimal> { _inputCount },
					Fill = new SolidColorPaint(new SKColor(col.R, col.G, col.B, col.A))
				});
			}
		}

		public HomePageViewModel()
		{
		}
		public HomePageViewModel(BudgetContext context, CategoryService categoryService)
		{
			_context = context;
			_categoryService = categoryService;
			SelectedCatgory = Categories.FirstOrDefault();
		}
	}
}
