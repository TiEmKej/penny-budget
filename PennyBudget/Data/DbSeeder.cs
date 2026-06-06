using System.Linq;
using PennyBudget.Models;

namespace PennyBudget.Data;

public static class DbSeeder
{
    public const string IncomeCategoryName = "Income";

    public static void Seed(AppDbContext db)
    {
        if (db.RecordCategory.Any(c => c.Name == IncomeCategoryName))
        {
            return;
        }
        
        db.RecordCategory.Add(new RecordCategory { Name = IncomeCategoryName, ColorHex = "#00E009", IsIncome = true});
        db.SaveChanges();
    }
}
