using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Platform.Storage;
using BudgetTracker.Interfaces;
using BudgetTracker.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
		private bool _isPieChart = false;
		[ObservableProperty]
		private string _inputTransactionNote = string.Empty;
		[ObservableProperty]
		private decimal _inputTransactionAmount = 0.01m;
		[ObservableProperty]
		private DateTimeOffset _inputTransactionDate = DateTime.Today;
		[ObservableProperty]
		[NotifyPropertyChangedFor(nameof(Transactions))]
		[NotifyPropertyChangedFor(nameof(Series))]
		[NotifyPropertyChangedFor(nameof(BarSeries))]
		private DateTimeOffset _startDate = DateTime.Today.AddDays(-7);
		[ObservableProperty]
		[NotifyPropertyChangedFor(nameof(Transactions))]
		[NotifyPropertyChangedFor(nameof(Series))]
		[NotifyPropertyChangedFor(nameof(BarSeries))]
		private DateTimeOffset _endDate = DateTime.Today;
		public ObservableCollection<CategoryViewModel> Categories
		{
			get
			{
				return new ObservableCollection<CategoryViewModel>(
					_categoryService
					.GetAll()
					.Select(cat => CategoryViewModel.FromModel(cat, _categoryService))
					);
			}
		}
		[ObservableProperty]
		private CategoryViewModel _selectedCatgory;
		public ObservableCollection<ISeries> Series
		{
			get
			{
				var transactionSeries = new ObservableCollection<ISeries>();
				foreach (var item in Categories)
				{
					IEnumerable<Transaction>? catTransactions = Transactions.Where(x => x.Category == item.Model);
					transactionSeries.Add(new PieSeries<decimal>
					{
						Values = new List<decimal> { catTransactions.Sum(x => x.Amount) },
						Name = item.Name,
						Fill = new SolidColorPaint(SKColor.Parse(item.Color.ToString()))
					});
				}
				return transactionSeries;
			}
			set
			{
				OnPropertyChanged(nameof(Series));
			}
		}
		public ObservableCollection<ISeries> BarSeries
		{
			get
			{
				var transactionSeries = new ObservableCollection<ISeries>();
				foreach (var item in Categories)
				{
					IEnumerable<Transaction>? catTransactions = Transactions.Where(x => x.Category == item.Model);
					transactionSeries.Add(new ColumnSeries<decimal>
					{
						Values = new List<decimal> { catTransactions.Sum(x => x.Amount) },
						Name = item.Name,
						Fill = new SolidColorPaint(SKColor.Parse(item.Color.ToString()))
					});
				}
				return transactionSeries;
			}
			set
			{
				OnPropertyChanged(nameof(BarSeries));
			}
		}
		public ObservableCollection<Transaction> Transactions { get; set; }
		public ObservableCollection<Transaction> SelectedTransactions { get; set; } = new ObservableCollection<Transaction>();
		[RelayCommand]
		public void ToggleQuickSettings()
		{
			IsQuickSettingsVisible = !IsQuickSettingsVisible;
		}
		[RelayCommand]
		public void ChangeDateButton(string daysString)
		{
			StartDate = DateTime.Today.AddDays(Convert.ToInt32(daysString));
			EndDate = DateTime.Today;
		}
		[RelayCommand]
		public async Task AddTransactionAsync()
		{
			IsBusy = true;
			try
			{
				var transaction = new Transaction
				{
					Category = SelectedCatgory.Model,
					Amount = InputTransactionAmount,
					Date = InputTransactionDate.DateTime,
					Note = InputTransactionNote
				};
				await _transactionService.AddAsync(transaction);
				Transactions.Add(transaction);
			} finally
			{
				RefreshTransactions();
				IsBusy = false;
			}
		}

		[RelayCommand]
		public async Task DeleteSelectedTransactionsAsync()
		{
			IsBusy = true;
			try
			{
				if (SelectedTransactions != null && SelectedTransactions.Any())
				{
					foreach (var transaction in SelectedTransactions.ToList())
					{
						await _transactionService.Delete(transaction);
						Transactions.Remove(transaction);
					}

				}
			} finally
			{
				RefreshTransactions();
				IsBusy = false;
			}
		}
		partial void OnStartDateChanged(DateTimeOffset value)
		{
			RefreshTransactions();
		}
		partial void OnEndDateChanged(DateTimeOffset value)
		{
			RefreshTransactions();
		}

		public void RefreshTransactions()
		{
			Transactions.Clear();
			var transactions = _transactionService.GetAll(StartDate.Date, EndDate.Date);
			foreach (var transaction in transactions)
			{
				Transactions.Add(transaction);
			}
			BarSeries = BarSeries; // Refresh bar series
			Series = Series; // Refresh pie series
		}

		public HomePageViewModel()
		{
			//Dummy data for designer
			SelectedCatgory = new CategoryViewModel
			{
				Name = "Uncategorized",
				ColorCode = Avalonia.Media.Colors.Red.ToUInt32()
			};

			Transactions = new ObservableCollection<Transaction>
			{
				new Transaction
				{
					Category = new Category()
					{
						Id = 1,
						Name = "Sample Category",
						ColorCode = Avalonia.Media.Colors.Blue.ToUInt32()
					},
					Amount = 100.00m,
					Date = DateTime.Today,
					Note = "Sample Transaction"
				}
			};
		}
		public HomePageViewModel(ICategoryService categoryService, ITransactionService transactionService)
		{
			_categoryService = categoryService;
			_transactionService = transactionService;
			Transactions = new ObservableCollection<Transaction>(_transactionService.GetAll(StartDate.Date, EndDate.Date));
			SelectedCatgory = Categories.FirstOrDefault();
		}
	}
}
