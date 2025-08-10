using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using BudgetTracker.ViewModels;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

	private async void ExportButton_Clicked(object sender, RoutedEventArgs args)
	{
		var topLevel = TopLevel.GetTopLevel(this);
		var vm = (this.DataContext as HomePageViewModel);
		try
		{
			vm.IsBusy = true;
			// Start async operation to open the dialog.
			var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
			{
				Title = "Save Text File",
				DefaultExtension = "csv",
				SuggestedFileName = "transactions.csv"
			});

			if (file is not null)
			{
				var csv = string.Concat(
					vm.Transactions.Select(tr => tr.Id + "," + 
															tr.Category.Name + "," + 
															tr.Amount + "," + 
															tr.Note + "," + 
															tr.Date + "\n"));
				// Open writing stream from the file.
				await using var stream = await file.OpenWriteAsync();
				using var streamWriter = new StreamWriter(stream);
				// Write some content to the file.
				await streamWriter.WriteLineAsync(csv);
			}
		} finally
		{
			vm.IsBusy = false;
		}
		
	}
}