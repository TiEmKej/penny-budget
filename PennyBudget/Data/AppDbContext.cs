using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using PennyBudget.Models;

namespace PennyBudget.Data;

public class AppDbContext : DbContext
{
    public DbSet<FinancialRecord> FinancialRecords => Set<FinancialRecord>();
    public DbSet<RecordCategory> RecordCategories => Set<RecordCategory>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Budget> Budgets => Set<Budget>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "PennyBudget", "penny.db");

        Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);
        options.UseSqlite($"Data Source={dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FinancialRecord>()
            .Property(r => r.Currency)
            .HasDefaultValue("PLN");

        modelBuilder.Entity<FinancialRecord>()
            .Property(r => r.CurrencyRate)
            .HasDefaultValue(1.0m);

        modelBuilder.Entity<FinancialRecord>()
            .Property(r => r.Date)
            .HasDefaultValueSql("date('now')");

        modelBuilder.Entity<FinancialRecord>()
            .HasOne(r => r.Account)
            .WithMany(a => a.Records)
            .HasForeignKey(r => r.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Account>()
            .Property(a => a.Currency)
            .HasDefaultValue("PLN");

        modelBuilder.Entity<Budget>()
            .Property(b => b.Currency)
            .HasDefaultValue("PLN");

        modelBuilder.Entity<Budget>()
            .HasOne(b => b.Category)
            .WithMany()
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}