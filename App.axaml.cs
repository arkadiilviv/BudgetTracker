using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using BudgetTracker.ViewModels;
using BudgetTracker.Views;
using Avalonia.Themes.Fluent;
using Avalonia.Themes.Simple;
using Classic.Avalonia.Theme;
using BudgetTracker.Helpers;
using Avalonia.Styling;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetTracker
{
	public partial class App : Application
	{
		public override void Initialize()
		{
			AvaloniaXamlLoader.Load(this);
			CustomSettings.Initialize();
			InitializeTheme();
		}

		private static void InitializeTheme()
		{
			switch (SettingsHelper.DefaultTheme)
			{
				case "Fluent":
				Current!.Styles.Add(new FluentTheme());
				break;
				case "Simple":
				Current!.Styles.Add(new SimpleTheme());
				break;
				case "Classic":
				Current!.Styles.Add(new ClassicTheme());
				Current!.RequestedThemeVariant = ThemeVariant.Light;
				break;
				default:
				Current!.Styles.Add(new FluentTheme());
				break;
			}
		}

		public override void OnFrameworkInitializationCompleted()
		{
			var collection = new ServiceCollection();
			collection.AddSingleton<MainWindowViewModel>();
			collection.AddSingleton<HomePageViewModel>();
			collection.AddSingleton<SettingsPageViewModel>();
			collection.AddSingleton<GuidePageViewModel>();
			collection.AddDbContext<BudgetContext>();

			var provider = collection.BuildServiceProvider();

			if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
			{
				// Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
				// More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
				DisableAvaloniaDataAnnotationValidation();
				desktop.MainWindow = new MainWindow
				{
					DataContext = provider.GetRequiredService<MainWindowViewModel>()
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