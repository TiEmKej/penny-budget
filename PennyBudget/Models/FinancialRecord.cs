using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PennyBudget.Models;

public class FinancialRecord
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Now;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "PLN";
    public decimal CurrencyRate { get; set; } = 1.0m;
    [NotMapped] public decimal AmountInYourCurrency => Amount * CurrencyRate;
    public int CategoryId { get; set; }
    public RecordCategory Category { get; set; } = null!;
    public bool IsCash  { get; set; }
}