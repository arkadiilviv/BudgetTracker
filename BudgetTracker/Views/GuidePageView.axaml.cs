using Avalonia.Controls;

namespace BudgetTracker.Views;

public partial class GuidePageView : UserControl
{
	public GuidePageView()
	{
		InitializeComponent();
	}

	private void CheckBox_Checked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
	{
		if (sender is CheckBox checkBox)
		{
			if (checkBox.IsChecked == true)
			{
				// Disable the guide
				Helpers.SettingsHelper.DisableGuide();
				checkBox.IsEnabled = false;
			}
		}
	}
}