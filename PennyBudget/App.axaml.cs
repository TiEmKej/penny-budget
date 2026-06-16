using System.Globalization;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using PennyBudget.Data;
using PennyBudget.ViewModels;
using PennyBudget.Views;

namespace PennyBudget;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        ApplyCulture();

        if (!Design.IsDesignMode)
        {
            using var db = new AppDbContext();
            db.Database.Migrate();
            DbSeeder.Seed(db);
        }

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static void ApplyCulture()
    {
        var language = SettingsManager.Load().Language;
        if (language is null) return;
        var culture = new CultureInfo(language);
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }
}
