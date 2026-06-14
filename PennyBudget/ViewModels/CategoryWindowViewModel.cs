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
using CategoryFormView = PennyBudget.Views.EditorForms.CategoryFormView;

namespace PennyBudget.ViewModels;

public partial class CategoryWindowViewModel : ViewModelBase, IRefreshable
{
    [ObservableProperty]
    public partial ObservableCollection<RecordCategory> Records { get; set; } = [];

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(DeleteCommand))]
    [NotifyCanExecuteChangedFor(nameof(EditCategoryCommand))]
    public partial RecordCategory? SelectedRecord { get; set; }

    public CategoryWindowViewModel() => _ = Load();

    public async Task Load()
    {
        await using var db = new AppDbContext();
        var records = await db.RecordCategories.ToListAsync();
        Records = new ObservableCollection<RecordCategory>(records);
    }

    [RelayCommand]
    private async Task AddCategory()
    {
        var mainWindow = (Application.Current?.ApplicationLifetime
            as IClassicDesktopStyleApplicationLifetime)?.MainWindow;

        var dialog = new CategoryFormView { DataContext = new Editors.CategoryFormViewModel() };
        var result = await dialog.ShowDialog<bool?>(mainWindow!);

        if (result == true)
        {
            var vm = (Editors.CategoryFormViewModel)dialog.DataContext;
            await using var db = new AppDbContext();
            await db.RecordCategories.AddAsync(vm.Record);
            await db.SaveChangesAsync();
            await Load();
        }
    }

    [RelayCommand(CanExecute = nameof(CanEditOrDelete))]
    private async Task EditCategory()
    {
        if (SelectedRecord is null) return;

        var mainWindow = (Application.Current?.ApplicationLifetime
            as IClassicDesktopStyleApplicationLifetime)?.MainWindow;

        var dialog = new CategoryFormView { DataContext = new Editors.CategoryFormViewModel(SelectedRecord) };
        var result = await dialog.ShowDialog<bool?>(mainWindow!);

        if (result == true)
        {
            var vm = (Editors.CategoryFormViewModel)dialog.DataContext;
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
        db.RecordCategories.Remove(SelectedRecord);
        await db.SaveChangesAsync();
        await Load();
    }

    private bool CanEditOrDelete() => SelectedRecord is not null;
}
