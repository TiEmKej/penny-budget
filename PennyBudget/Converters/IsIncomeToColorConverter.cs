using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace PennyBudget.Converters;

public class IsIncomeToColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is true ? new SolidColorBrush(Color.Parse("#4CAF50")) : new SolidColorBrush(Color.Parse("#F44336"));

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
