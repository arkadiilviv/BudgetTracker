using Avalonia.Media;
using BudgetTracker.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore.SkiaSharpView.Painting;
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

		[ObservableProperty]
		private ObservableCollection<Category> _categories = new ObservableCollection<Category>
		{
			new Category { Id = 1, Name = "Food", Icon = "\ue900", Color = Colors.Red },
			new Category { Id = 2, Name = "Transport", Icon = "\ue901", Color = Colors.Violet },
			new Category { Id = 3, Name = "Entertainment", Icon = "\ue902", Color = Colors.Blue },
			new Category { Id = 4, Name = "Utilities", Icon = "\ue903", Color = Colors.GreenYellow }
		};
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
				var col = SelectedCatgory.Color;
				Series.Add(new CustomPieSeries<decimal>()
				{
					Name = _inputText,
					Values = new List<Decimal> { _inputCount },
					Fill = new SolidColorPaint(new SKColor(col.R, col.G, col.B, col.A))
				});
			}
		}

	}
}
