using System.Linq;
using PennyBudget.Models;

namespace PennyBudget.Data;

public static class DbSeeder
{
    private const string IncomeCategoryName = "Income";

    public static void Seed(AppDbContext db)
    {
        if (db.RecordCategories.Any(c => c.Name == IncomeCategoryName))
        {
            return;
        }
        
        db.RecordCategories.Add(new RecordCategory { Name = IncomeCategoryName, ColorHex = "#00E009", IsIncome = true});
        db.SaveChanges();
    }
}
