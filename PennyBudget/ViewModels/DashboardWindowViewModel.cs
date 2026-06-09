using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using PennyBudget.Data;
using PennyBudget.Models;

namespace PennyBudget.ViewModels;

public partial class DashboardWindowViewModel : ViewModelBase, IRefreshable
{
    [ObservableProperty]
    public partial ObservableCollection<FinancialRecord> Records { get; set; } = [];

    [ObservableProperty] public partial decimal IncomeSum { get; set; } = 0;
    [ObservableProperty] public partial decimal ExpenseSum { get; set; } = 0;
    [ObservableProperty] public partial decimal Balance { get; set; }
    [ObservableProperty] public partial bool IsInDebt { get; set; }
    
    public async Task Load()
    {
        await using var db = new AppDbContext();
        var data = await db.FinancialRecords.Include(r => r.Category).OrderByDescending(r => r.Date).ToListAsync();
        Records = new ObservableCollection<FinancialRecord>(data);

        IncomeSum = Records.Where(p => p.AmountInYourCurrency > 0.0m).Sum(p => p.AmountInYourCurrency);
        ExpenseSum = Records.Where(p => p.AmountInYourCurrency < 0.0m).Sum(p => p.AmountInYourCurrency);
        Balance = IncomeSum + ExpenseSum;
        IsInDebt = !(Balance < 0);
    }
    
    
}