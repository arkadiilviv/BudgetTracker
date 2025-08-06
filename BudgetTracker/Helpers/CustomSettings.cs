using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BudgetTracker.Helpers
{
	public static class SettingsHelper
	{
		public static string DefaultTheme { get; set; }
		public static string DefaultCurrency { get; set; }
		public static string DefaultLanguage { get; set; }
		public static void SetTheme(string theme)
		{
			var settings = new CustomSettings
			{
				DefaultTheme = theme,
				DefaultCurrency = DefaultCurrency,
				DefaultLanguage = DefaultLanguage
			};
			string fileName = "CustomSettings.json";
			using FileStream fileStream = File.Open(fileName, FileMode.Truncate);
			JsonSerializer.Serialize(fileStream, settings);
		}
	}
	public class CustomSettings
	{
		public static async void Initialize()
		{
			string fileName = "CustomSettings.json";
			if (File.Exists(fileName))
			{
				await using FileStream fileStream = File.OpenRead(fileName);
				var settings = JsonSerializer.Deserialize<CustomSettings>(fileStream);
				fileStream.Close();
				SettingsHelper.DefaultTheme = settings?.DefaultTheme ?? "Fluent";
				SettingsHelper.DefaultCurrency = settings?.DefaultCurrency ?? "EUR";
				SettingsHelper.DefaultLanguage = settings?.DefaultLanguage ?? "en-US";
			} else
			{
				await CreateSettings(fileName);
			}
		}

		private static async Task CreateSettings(string fileName)
		{
			var settings = new CustomSettings
			{
				DefaultTheme = "Fluent",
				DefaultCurrency = "EUR",
				DefaultLanguage = "en-US"
			};
			await using FileStream fileStream = File.Create(fileName);
			await JsonSerializer.SerializeAsync(fileStream, settings);
			SettingsHelper.DefaultTheme = settings?.DefaultTheme ?? "Fluent";
			SettingsHelper.DefaultCurrency = settings?.DefaultCurrency ?? "EUR";
			SettingsHelper.DefaultLanguage = settings?.DefaultLanguage ?? "en-US";
		}

		[JsonInclude]
		public string DefaultTheme { get; set; }
		[JsonInclude]
		public string DefaultCurrency { get; set; }
		[JsonInclude]
		public string DefaultLanguage { get; set; }
	}
}
