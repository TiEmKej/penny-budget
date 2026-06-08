using Avalonia.Controls;
using Avalonia.Interactivity;

namespace PennyBudget.Views.EditorForms;

public partial class RecordFormView : Window
{
    public RecordFormView()
    {
        InitializeComponent();
    }

    private void OnSave(object sender, RoutedEventArgs e)   => Close(true);
    private void OnCancel(object sender, RoutedEventArgs e) => Close(false);
}
