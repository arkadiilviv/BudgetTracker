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
using System.Threading;
using System.Threading.Tasks;

namespace BudgetTracker.ViewModels
{
	public partial class HomePageViewModel : ViewModelBase
	{
		private ICategoryService _categoryService;
		private ITransactionService _transactionService;
		[ObservableProperty]
		private bool _isQuickSettingsVisible = true;
		[ObservableProperty]
		private bool _isBusy = false;
		[ObservableProperty]
		private bool _isPieChart = false;
		[ObservableProperty]
		private Category _selectedCatgory = new Category();
		[ObservableProperty]
		private string _inputTransactionNote = string.Empty;
		[ObservableProperty]
		private decimal _inputTransactionAmount = 0.01m;
		[ObservableProperty]
		private DateTimeOffset _inputTransactionDate = DateTime.Today;
		[ObservableProperty]
		[NotifyPropertyChangedFor(nameof(Transactions))]
		private DateTimeOffset _startDate = DateTime.Today.AddDays(-7);
		[ObservableProperty]
		[NotifyPropertyChangedFor(nameof(Transactions))]
		private DateTimeOffset _endDate = DateTime.Today;
		[ObservableProperty]
		private ObservableCollection<CustomPieSeries<decimal>> _series = new ObservableCollection<CustomPieSeries<decimal>>();
		public ObservableCollection<Category> Categories { get => new ObservableCollection<Category>(_categoryService.GetAll()); }
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
					Category = SelectedCatgory,
					Amount = InputTransactionAmount,
					Date = InputTransactionDate.DateTime,
					Note = InputTransactionNote
				};
				await _transactionService.AddAsync(transaction);
				Transactions.Add(transaction);
			} finally
			{
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
		}

		public HomePageViewModel()
		{
			//Dummy data for designer
			SelectedCatgory = new Category
			{
				Name = "Uncategorized",
				ColorCode = Avalonia.Media.Colors.Red.ToUInt32()
			};
		}
		public HomePageViewModel(ICategoryService categoryService, ITransactionService transactionService)
		{
			_categoryService = categoryService;
			_transactionService = transactionService;
			SelectedCatgory = Categories.FirstOrDefault();
			Transactions = new ObservableCollection<Transaction>(_transactionService.GetAll(StartDate.Date, EndDate.Date));
		}
	}
}
