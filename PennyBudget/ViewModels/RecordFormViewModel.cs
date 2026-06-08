using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using PennyBudget.Data;
using PennyBudget.Models;

namespace PennyBudget.ViewModels;

public partial class RecordFormViewModel : ViewModelBase
{
    [ObservableProperty] private FinancialRecord _record = new();
    [ObservableProperty] private ObservableCollection<RecordCategory> _categories = [];
    [ObservableProperty] private RecordCategory? _selectedCategory;
    [ObservableProperty] private DateTimeOffset? _recordDate = DateTimeOffset.Now;

    public string Title => Record.Id == 0 ? "Add Record" : "Edit Record";

    public RecordFormViewModel() => LoadCategories();

    public RecordFormViewModel(FinancialRecord record)
    {
        Record = record;
        RecordDate = new DateTimeOffset(record.Date);
        LoadCategories();
    }

    private void LoadCategories()
    {
        using var db = new AppDbContext();
        Categories = new ObservableCollection<RecordCategory>(db.RecordCategory.ToList());
        if (Record.CategoryId != 0)
            SelectedCategory = Categories.FirstOrDefault(c => c.Id == Record.CategoryId);
    }

    partial void OnRecordDateChanged(DateTimeOffset? value)
    {
        if (value.HasValue)
            Record.Date = value.Value.DateTime;
    }

    partial void OnSelectedCategoryChanged(RecordCategory? value)
    {
        if (value is not null)
            Record.CategoryId = value.Id;
    }
}
