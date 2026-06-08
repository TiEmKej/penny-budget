using Avalonia.Controls;
using Avalonia.Interactivity;

namespace PennyBudget.Views.EditorForms;

public partial class CategoryFormView : Window
{
    public CategoryFormView()
    {
        InitializeComponent();
    }

    private void OnSave(object sender, RoutedEventArgs e)   => Close(true);
    private void OnCancel(object sender, RoutedEventArgs e) => Close(false);
}
