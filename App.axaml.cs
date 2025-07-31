using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using BudgetTracker.Models;
using BudgetTracker.ViewModels;
using BudgetTracker.Views;

namespace BudgetTracker
{
	public partial class App : Application
	{
		public override void Initialize()
		{
			AvaloniaXamlLoader.Load(this);
			CustomSettings.Initialize();
			if (SettingsHelper.DefaultTheme == "Fluent")
			{
				Current!.Styles.Add(new Avalonia.Themes.Fluent.FluentTheme());
			} else if (SettingsHelper.DefaultTheme == "Simple")
			{
				Current!.Styles.Add(new Avalonia.Themes.Simple.SimpleTheme());
			}
		}

		public override void OnFrameworkInitializationCompleted()
		{
			if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
			{
				// Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
				// More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
				DisableAvaloniaDataAnnotationValidation();
				desktop.MainWindow = new MainWindow
				{
					DataContext = new MainWindowViewModel(),
				};
			}

			base.OnFrameworkInitializationCompleted();
		}

		private void DisableAvaloniaDataAnnotationValidation()
		{
			// Get an array of plugins to remove
			var dataValidationPluginsToRemove =
				BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

			// remove each entry found
			foreach (var plugin in dataValidationPluginsToRemove)
			{
				BindingPlugins.DataValidators.Remove(plugin);
			}
		}
	}
}