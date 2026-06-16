using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using PennyBudget.Data;
using PennyBudget.Models;

namespace PennyBudget.ViewModels;

public record LanguageOption(string DisplayName, string? Code);

public partial class SettingsWindowViewModel : ViewModelBase
{
    public ObservableCollection<LanguageOption> Languages { get; } =
    [
        new("System Default", null),
        new("English", "en"),
        new("Polski", "pl"),
    ];

    [ObservableProperty]
    public partial LanguageOption? SelectedLanguage { get; set; }

    [ObservableProperty]
    public partial bool RestartRequired { get; set; }

    public SettingsWindowViewModel()
    {
        var settings = SettingsManager.Load();
        SelectedLanguage = Languages.FirstOrDefault(l => l.Code == settings.Language) ?? Languages[0];
    }

    partial void OnSelectedLanguageChanged(LanguageOption? value)
    {
        if (value is null) return;
        var settings = SettingsManager.Load();
        if (settings.Language == value.Code) return;
        settings.Language = value.Code;
        SettingsManager.Save(settings);
        RestartRequired = true;
    }
}
