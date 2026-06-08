using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PennyBudget.Models;

public class FinancialRecord
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public RecordCategory Category { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    [NotMapped] public decimal AmountInYourCurrency => Amount * CurrencyRate;
    public string Currency { get; set; } = "PLN";
    public decimal CurrencyRate { get; set; } = 1.0m;
    public DateTime Date { get; set; } = DateTime.Now;
}