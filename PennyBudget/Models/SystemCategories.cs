using System.Collections.Generic;
using System.Resources;

namespace PennyBudget.Models;

public static class SystemCategories
{
    public record Info(string ColorHex, bool IsIncome);

    private static readonly ResourceManager ResourceManager =
        new("PennyBudget.Resources.SystemCategoryNames", typeof(SystemCategories).Assembly);

    public static readonly IReadOnlyDictionary<string, Info> All = new Dictionary<string, Info>
    {
        ["income"]        = new("#4CAF50", true),
        ["salary"]        = new("#2196F3", true),
        ["transfer_in"]   = new("#00BCD4", true),
        ["transfer_out"]  = new("#FF9800", false),
        ["groceries"]     = new("#8BC34A", false),
        ["transport"]     = new("#9C27B0", false),
        ["medical"]       = new("#F44336", false),
        ["entertainment"] = new("#FF5722", false),
        ["bills"]         = new("#607D8B", false),
        ["debt_payments"] = new("#795548", false),
    };

    public static string GetDisplayName(string key) =>
        ResourceManager.GetString(key) ?? key;

    public static string GetColorHex(string key) =>
        All.TryGetValue(key, out var info) ? info.ColorHex : "#000000";
}
