using Microsoft.EntityFrameworkCore;

namespace PennyBudget.Models;

[Index(nameof(CategoryId), nameof(Month), nameof(Year), IsUnique = true)]
public class Budget
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public RecordCategory Category { get; set; } = null!;
    public decimal LimitAmount { get; set; }
    public string Currency { get; set; } = "PLN";
    public int Month { get; set; }
    public int Year { get; set; }
}
