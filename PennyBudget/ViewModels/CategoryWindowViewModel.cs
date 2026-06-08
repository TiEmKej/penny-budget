using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using PennyBudget.Data;
using PennyBudget.Models;
using PennyBudget.Views;

namespace PennyBudget.ViewModels;

public partial class CategoryWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<RecordCategory> _records = [];

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(DeleteCommand))]
    [NotifyCanExecuteChangedFor(nameof(EditCategoryCommand))]
    private RecordCategory? _selectedRecord;

    public CategoryWindowViewModel() => Load();

    private void Load()
    {
        using var db = new AppDbContext();
        Records = new ObservableCollection<RecordCategory>(db.RecordCategory.ToList());
    }

    [RelayCommand]
    private async Task AddCategory()
    {
        var mainWindow = (Application.Current?.ApplicationLifetime
            as IClassicDesktopStyleApplicationLifetime)?.MainWindow;

        var dialog = new CategoryFormView { DataContext = new CategoryFormViewModel() };
        var result = await dialog.ShowDialog<bool?>(mainWindow!);

        if (result == true)
        {
            var vm = (CategoryFormViewModel)dialog.DataContext;
            await using var db = new AppDbContext();
            await db.RecordCategory.AddAsync(vm.Record);
            await db.SaveChangesAsync();
            Load();
        }
    }

    [RelayCommand(CanExecute = nameof(CanEditOrDelete))]
    private async Task EditCategory()
    {
        if (SelectedRecord is null) return;

        var mainWindow = (Application.Current?.ApplicationLifetime
            as IClassicDesktopStyleApplicationLifetime)?.MainWindow;

        var dialog = new CategoryFormView { DataContext = new CategoryFormViewModel(SelectedRecord) };
        var result = await dialog.ShowDialog<bool?>(mainWindow!);

        if (result == true)
        {
            var vm = (CategoryFormViewModel)dialog.DataContext;
            await using var db = new AppDbContext();
            db.Entry(vm.Record).State = EntityState.Modified;
            await db.SaveChangesAsync();
            Load();
        }
    }

    [RelayCommand(CanExecute = nameof(CanEditOrDelete))]
    private async Task Delete()
    {
        if (SelectedRecord is null) return;

        await using var db = new AppDbContext();
        db.RecordCategory.Remove(SelectedRecord);
        await db.SaveChangesAsync();
        Load();
    }

    private bool CanEditOrDelete() => SelectedRecord is not null;
}
