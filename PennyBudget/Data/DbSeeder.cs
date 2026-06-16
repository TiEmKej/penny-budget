using System.Linq;
using PennyBudget.Models;

namespace PennyBudget.Data;

public static class DbSeeder
{
    public static void Seed(AppDbContext db)
    {
        if (db.RecordCategories.Any(c => c.IsSystem))
            return;

        foreach (var (key, info) in SystemCategories.All)
        {
            db.RecordCategories.Add(new RecordCategory
            {
                IsSystem = true,
                Key = key,
                IsIncome = info.IsIncome
            });
        }

        db.SaveChanges();
    }
}
