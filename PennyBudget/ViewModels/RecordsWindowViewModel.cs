using System;
using System.Collections.Generic;
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
using RecordFormView = PennyBudget.Views.EditorForms.RecordFormView;

namespace PennyBudget.ViewModels;

public partial class RecordsWindowViewModel : ViewModelBase, IRefreshable
{
    private List<FinancialRecord> _allRecords = [];

    [ObservableProperty]
    public partial ObservableCollection<FinancialRecord> Records { get; set; } = [];

    [ObservableProperty]
    public partial ObservableCollection<CategorySummary> CategorySummaries { get; set; } = [];

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(DeleteCommand))]
    [NotifyCanExecuteChangedFor(nameof(EditRecordCommand))]
    public partial FinancialRecord? SelectedRecord { get; set; }

    [ObservableProperty]
    public partial string SearchText { get; set; } = "";

    partial void OnSearchTextChanged(string value) => ApplyFilter();

    public RecordsWindowViewModel() => _ = Load();

    public async Task Load()
    {
        await using var db = new AppDbContext();
        _allRecords = await db.FinancialRecords.Include(r => r.Category).OrderByDescending(r => r.Date).ToListAsync();
        ApplyFilter();
        RefreshCategorySummaries();
    }

    private void ApplyFilter()
    {
        var term = SearchText.Trim();
        var filtered = string.IsNullOrEmpty(term)
            ? _allRecords
            : _allRecords.Where(r =>
                r.Description.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                r.Category.Name.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                r.Currency.Contains(term, StringComparison.OrdinalIgnoreCase));
        Records = new ObservableCollection<FinancialRecord>(filtered);
    }

    private void RefreshCategorySummaries()
    {
        CategorySummaries = new ObservableCollection<CategorySummary>(
            _allRecords
                .GroupBy(r => r.Category)
                .Select(g => new CategorySummary
                {
                    CategoryName = g.Key.Name,
                    IsIncome = g.Key.IsIncome,
                    TotalAmountInPln = g.Sum(r => r.AmountInYourCurrency)
                })
                .OrderBy(s => s.CategoryName));
    }

    [RelayCommand]
    private async Task AddRecord()
    {
        var mainWindow = (Application.Current?.ApplicationLifetime
            as IClassicDesktopStyleApplicationLifetime)?.MainWindow;

        var dialog = new RecordFormView { DataContext = new Editors.RecordFormViewModel() };
        var result = await dialog.ShowDialog<bool?>(mainWindow!);

        if (result == true)
        {
            var vm = (Editors.RecordFormViewModel)dialog.DataContext;
            await using var db = new AppDbContext();
            await db.FinancialRecords.AddAsync(vm.Record);
            await db.SaveChangesAsync();
            await Load();
        }
    }

    [RelayCommand(CanExecute = nameof(CanEditOrDelete))]
    private async Task EditRecord()
    {
        if (SelectedRecord is null) return;

        var mainWindow = (Application.Current?.ApplicationLifetime
            as IClassicDesktopStyleApplicationLifetime)?.MainWindow;

        var dialog = new RecordFormView { DataContext = new Editors.RecordFormViewModel(SelectedRecord) };
        var result = await dialog.ShowDialog<bool?>(mainWindow!);

        if (result == true)
        {
            var vm = (Editors.RecordFormViewModel)dialog.DataContext;
            await using var db = new AppDbContext();
            db.Entry(vm.Record).State = EntityState.Modified;
            await db.SaveChangesAsync();
            await Load();
        }
    }

    [RelayCommand(CanExecute = nameof(CanEditOrDelete))]
    private async Task Delete()
    {
        if (SelectedRecord is null) return;

        await using var db = new AppDbContext();
        db.FinancialRecords.Remove(SelectedRecord);
        await db.SaveChangesAsync();
        await Load();
    }

    private bool CanEditOrDelete() => SelectedRecord is not null;
}
