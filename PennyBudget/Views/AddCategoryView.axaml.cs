using Avalonia.Controls;
using Avalonia.Interactivity;

namespace PennyBudget.Views;

public partial class AddCategoryView : Window
{
    public AddCategoryView()
    {
        InitializeComponent();
    }
    
    private void OnSave(object sender, RoutedEventArgs e)   => Close(true);
    private void OnCancel(object sender, RoutedEventArgs e) => Close(false);
}