using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using PennyBudget.Models;

namespace PennyBudget.ViewModels.Editors;

public partial class CategoryFormViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial RecordCategory Record { get; set; } = new();

    [ObservableProperty]
    public partial Color SelectedColor { get; set; } = Colors.Black;

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
