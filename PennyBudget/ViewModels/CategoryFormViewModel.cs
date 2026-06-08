using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using PennyBudget.Models;

namespace PennyBudget.ViewModels;

public partial class CategoryFormViewModel : ViewModelBase
{
    [ObservableProperty] private RecordCategory _record = new();
    [ObservableProperty] private Color _selectedColor = Colors.Black;

    public string Title => Record.Id == 0 ? "Add Category" : "Edit Category";

    public CategoryFormViewModel() { }

    public CategoryFormViewModel(RecordCategory category)
    {
        Record = category;
        SelectedColor = Color.Parse(category.ColorHex);
    }

    partial void OnSelectedColorChanged(Color value)
    {
        Record.ColorHex = $"#{value.R:X2}{value.G:X2}{value.B:X2}";
    }
}
