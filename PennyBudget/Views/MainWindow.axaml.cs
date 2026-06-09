using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using PennyBudget.ViewModels;

namespace PennyBudget.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        var tabControl = this.FindControl<TabControl>("MainTabControl");
        if (tabControl is not null)
            tabControl.SelectionChanged += OnTabSelectionChanged;
    }

    private void OnTabSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is not TabControl tabControl) return;
        if (tabControl.SelectedContent is StyledElement { DataContext: IRefreshable vm })
            vm.Load();
    }
}