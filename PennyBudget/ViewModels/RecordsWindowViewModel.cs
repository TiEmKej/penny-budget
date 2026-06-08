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

public partial class RecordsWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<FinancialRecord> _records = [];

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(DeleteCommand))]
    [NotifyCanExecuteChangedFor(nameof(EditRecordCommand))]
    private FinancialRecord? _selectedRecord;

    public RecordsWindowViewModel() => Load();

    private void Load()
    {
        using var db = new AppDbContext();
        Records = new ObservableCollection<FinancialRecord>(
            db.FinancialRecords.Include(r => r.Category).ToList());
    }

    [RelayCommand]
    private async Task AddRecord()
    {
        var mainWindow = (Application.Current?.ApplicationLifetime
            as IClassicDesktopStyleApplicationLifetime)?.MainWindow;

        var dialog = new RecordFormView { DataContext = new RecordFormViewModel() };
        var result = await dialog.ShowDialog<bool?>(mainWindow!);

        if (result == true)
        {
            var vm = (RecordFormViewModel)dialog.DataContext;
            await using var db = new AppDbContext();
            await db.FinancialRecords.AddAsync(vm.Record);
            await db.SaveChangesAsync();
            Load();
        }
    }

    [RelayCommand(CanExecute = nameof(CanEditOrDelete))]
    private async Task EditRecord()
    {
        if (SelectedRecord is null) return;

        var mainWindow = (Application.Current?.ApplicationLifetime
            as IClassicDesktopStyleApplicationLifetime)?.MainWindow;

        var dialog = new RecordFormView { DataContext = new RecordFormViewModel(SelectedRecord) };
        var result = await dialog.ShowDialog<bool?>(mainWindow!);

        if (result == true)
        {
            var vm = (RecordFormViewModel)dialog.DataContext;
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
        db.FinancialRecords.Remove(SelectedRecord);
        await db.SaveChangesAsync();
        Load();
    }

    private bool CanEditOrDelete() => SelectedRecord is not null;
}
