using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PennyBudget.Models;

public class RecordCategory
{
    public int Id { get; set; }
    public bool IsSystem { get; set; }
    public string? Key { get; set; }
    public string? Name { get; set; }
    public string? ColorHex { get; set; }
    public bool IsIncome { get; set; }
    public ICollection<FinancialRecord> Records { get; set; } = [];

    [NotMapped] public string DisplayName =>
        IsSystem && Key != null ? SystemCategories.GetDisplayName(Key) : (Name ?? "");

    [NotMapped] public string DisplayColorHex =>
        IsSystem && Key != null ? SystemCategories.GetColorHex(Key) : (ColorHex ?? "#000000");
}
