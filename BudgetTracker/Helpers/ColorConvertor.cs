using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker.Helpers
{
	public class ColorConvertor : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value is uint ui)
			{
				var color = Color.FromUInt32(ui);
				return new SolidColorBrush(color);
			}
			return Avalonia.Media.Brushes.Transparent;
		}

		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
			=> throw new NotSupportedException();
	}
}
