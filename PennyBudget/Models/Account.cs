using System.Collections.Generic;

namespace PennyBudget.Models;

public class Account
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public AccountType Type { get; set; } = AccountType.Transactional;
    public string Currency { get; set; } = "PLN";
    public ICollection<FinancialRecord> Records { get; set; } = [];
}

public enum AccountType
{
    Transactional,
    Savings
}
