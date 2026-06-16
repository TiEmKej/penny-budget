using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace PennyBudget.Behaviors;

public static class DecimalInputBehavior
{
    public static readonly AttachedProperty<bool> NormalizeDecimalSeparatorProperty =
        AvaloniaProperty.RegisterAttached<NumericUpDown, bool>(
            "NormalizeDecimalSeparator", typeof(DecimalInputBehavior));

    private static readonly EventHandler<TextInputEventArgs> Handler = OnTextInput;

    static DecimalInputBehavior()
    {
        NormalizeDecimalSeparatorProperty.Changed.AddClassHandler<NumericUpDown>(OnPropertyChanged);
    }

    public static bool GetNormalizeDecimalSeparator(NumericUpDown control) =>
        control.GetValue(NormalizeDecimalSeparatorProperty);

    public static void SetNormalizeDecimalSeparator(NumericUpDown control, bool value) =>
        control.SetValue(NormalizeDecimalSeparatorProperty, value);

    private static void OnPropertyChanged(NumericUpDown control, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.NewValue is true)
            control.AddHandler(InputElement.TextInputEvent, Handler, RoutingStrategies.Tunnel);
        else
            control.RemoveHandler(InputElement.TextInputEvent, Handler);
    }

    private static void OnTextInput(object? sender, TextInputEventArgs e)
    {
        if (e.Text is not ("." or ",")) return;
        var expected = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        if (e.Text != expected)
            e.Text = expected;
    }
}
