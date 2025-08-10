using Avalonia.Controls;
using BudgetTracker.ViewModels;

namespace BudgetTracker.Views;

public partial class HomePageView : UserControl
{
	public HomePageView()
	{
		InitializeComponent();
	}

	// Resolving splitview bug of outside clicks not changing binding
	private void SplitView_PaneClosed(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
	{
		if (sender is SplitView splitView)
		{
			splitView.IsPaneOpen = false;
			(this.DataContext as HomePageViewModel).IsQuickSettingsVisible = false;
		}
	}

	private void SplitView_PaneOpened(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
	{

		if (sender is SplitView splitView)
		{
			splitView.IsPaneOpen = true;
			(this.DataContext as HomePageViewModel).IsQuickSettingsVisible = true;
		}
	}
}