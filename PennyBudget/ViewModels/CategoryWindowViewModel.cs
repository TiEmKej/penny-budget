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

public partial class CategoryWindowViewModel: ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<RecordCategory> _records = [];

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(DeleteCommand))]
    private RecordCategory? _selectedRecord;

    public CategoryWindowViewModel() => Load();

    private void Load()
    {
        using var db = new AppDbContext();
        Records = new ObservableCollection<RecordCategory>(
            db.RecordCategory.ToList());
    }

    [RelayCommand]
    private async Task AddCategory()
    {
        var mainWindow = (Application.Current?.ApplicationLifetime
            as IClassicDesktopStyleApplicationLifetime)?.MainWindow;

        var dialog = new AddCategoryView { DataContext = new AddCategoryViewModel() };
        var result = await dialog.ShowDialog<bool?>(mainWindow!);

        if (result == true)
        {
            var vm = (AddCategoryViewModel)dialog.DataContext;
            await using var db = new AppDbContext();
            await db.RecordCategory.AddAsync(vm.Record);
            await db.SaveChangesAsync();
        }
    }

    [RelayCommand(CanExecute = nameof(CanDelete))]
    private async Task Delete()
    {
        if (SelectedRecord is null)
        {
            return;
        }

        await using var db = new AppDbContext();
        db.RecordCategory.Remove(SelectedRecord);
        await db.SaveChangesAsync();
        Load();
    }

    private bool CanDelete() => SelectedRecord is not null;
}