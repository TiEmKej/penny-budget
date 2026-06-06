using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using PennyBudget.Models;

namespace PennyBudget.ViewModels;

public partial class AddCategoryViewModel : ViewModelBase
{
    [ObservableProperty] private RecordCategory _record = new();
    [ObservableProperty] private Color _selectedColor = Colors.Black;

    partial void OnSelectedColorChanged(Color value)
    {
        Record.ColorHex = $"#{value.R:X2}{value.G:X2}{value.B:X2}";
    }
}