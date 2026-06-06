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

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<FinancialRecord> _records = [];

    public MainWindowViewModel()
    {
        LoadRecords();
    }

    private void LoadRecords()
    {
        using var db = new AppDbContext();
        var list = db.FinancialRecords
            .Include(r => r.Category)
            .ToList();
        Records = new ObservableCollection<FinancialRecord>(list);
    }
    
    [RelayCommand]
    private async Task AddRecord()
    {
        var mainWindow = (Application.Current?.ApplicationLifetime
            as IClassicDesktopStyleApplicationLifetime)?.MainWindow;

        var dialog = new AddRecordView { DataContext = new AddRecordViewModel() };
        var result = await dialog.ShowDialog<bool?>(mainWindow!);

        if (result == true)
        {
            var vm = (AddRecordViewModel)dialog.DataContext;
            await using var db = new AppDbContext();
            await db.FinancialRecords.AddAsync(vm.Record);
            await db.SaveChangesAsync();
            LoadRecords();
        }
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
}
