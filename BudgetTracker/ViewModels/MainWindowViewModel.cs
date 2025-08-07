using BudgetTracker.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore.SkiaSharpView;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BudgetTracker.ViewModels
{
	public class CustomPieSeries<T> : PieSeries<T>, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		public Category Category { get; set; } = new Category();
		protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

		public T? TransactionCount => Values != null ? Values.FirstOrDefault() : default;
		public void SetValues(IEnumerable<T> newValues)
		{
			Values = (IReadOnlyCollection<T>?)newValues;
			OnPropertyChanged(nameof(TransactionCount));
		}
	}
	public partial class MainWindowViewModel : ViewModelBase
	{
		[ObservableProperty]
		[NotifyPropertyChangedFor(nameof(MenuButtonText))]
		private bool _isMenuVisible = true;
		// <--   -->
		public string MenuButtonText => _isMenuVisible ? "\uf060;" : "\uf061;";
		[ObservableProperty]
		private ViewModelBase _currentViewModel;

		private readonly HomePageViewModel _homeViewModel;
		private readonly SettingsPageViewModel _settingsViewModel;
		private readonly GuidePageViewModel _guideViewModel;


		[RelayCommand]
		public void ToggleMenu()
		{
			IsMenuVisible = !IsMenuVisible;
		}
		[RelayCommand]
		public void GoHome()
		{
			CurrentViewModel = _homeViewModel;
		}
		[RelayCommand]
		public void GoSettings()
		{
			CurrentViewModel = _settingsViewModel;
		}
		public MainWindowViewModel() { }
		public MainWindowViewModel(HomePageViewModel homePageViewModel, SettingsPageViewModel settingsPageViewModel, GuidePageViewModel guideViewModel)
		{
			_homeViewModel = homePageViewModel;
			_settingsViewModel = settingsPageViewModel;
			_guideViewModel = guideViewModel;
			CurrentViewModel = _guideViewModel;
		}

	}
}
