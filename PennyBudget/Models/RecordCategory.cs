using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PennyBudget.Models;

[Index(nameof(Name), IsUnique = true)]
public class RecordCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ColorHex { get; set; } = "#000000";
    public bool IsIncome { get; set; }
    public ICollection<FinancialRecord> Records { get; set; } = [];
}