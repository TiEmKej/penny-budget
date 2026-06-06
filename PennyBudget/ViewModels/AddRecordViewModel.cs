using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using PennyBudget.Data;
using PennyBudget.Models;

namespace PennyBudget.ViewModels;

public partial class AddRecordViewModel : ViewModelBase
{
    [ObservableProperty] private FinancialRecord _record = new();
    [ObservableProperty] private ObservableCollection<RecordCategory> _categories = [];
    [ObservableProperty] private RecordCategory? _selectedCategory;
    [ObservableProperty] private bool _isExpense = true;
    [ObservableProperty] private DateTimeOffset? _recordDate = DateTimeOffset.Now;

    partial void OnRecordDateChanged(DateTimeOffset? value)
    {
        if (value.HasValue)
            Record.Date = value.Value.DateTime;
    }

    public AddRecordViewModel()
    {
        LoadCategories();
    }

    private void LoadCategories()
    {
        using var db = new AppDbContext();
        Categories = new ObservableCollection<RecordCategory>(db.RecordCategory.ToList());
    }

    partial void OnSelectedCategoryChanged(RecordCategory? value)
    {
        if (value is not null)
        {
            Record.CategoryId = value.Id;
        }
    }
}