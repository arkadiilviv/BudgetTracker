using Avalonia.Media;
using BudgetTracker.Interfaces;
using BudgetTracker.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BudgetTracker.ViewModels
{
	public partial class HomePageViewModel : ViewModelBase
	{
		private ICategoryService _categoryService;
		private ITransactionService _transactionService;
		[ObservableProperty]
		private bool _isQuickSettingsVisible = false;
		[ObservableProperty]
		private bool _isBusy = false;
		[ObservableProperty]
		private Category _selectedCatgory = new Category();
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

		public ObservableCollection<Category> Categories { get => new ObservableCollection<Category>(_categoryService.GetAll()); }

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
				Color col = Avalonia.Media.Color.FromUInt32(SelectedCatgory.ColorCode ?? 0);
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
		public HomePageViewModel(ICategoryService categoryService, ITransactionService transactionService)
		{
			_categoryService = categoryService;
			_transactionService = transactionService;
			SelectedCatgory = Categories.FirstOrDefault();
		}
	}
}
