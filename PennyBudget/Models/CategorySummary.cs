namespace PennyBudget.Models;

public class CategorySummary
{
    public string CategoryName { get; set; } = string.Empty;
    public bool IsIncome { get; set; }
    public decimal TotalAmountInPln { get; set; }
}
