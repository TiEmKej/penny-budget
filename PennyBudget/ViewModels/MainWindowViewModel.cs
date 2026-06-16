namespace PennyBudget.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public DashboardWindowViewModel Dashboard { get; } = new();
    public RecordsWindowViewModel Records { get; } = new();
    public CategoryWindowViewModel Categories { get; } = new();
    public SettingsWindowViewModel Settings { get; } = new();
}
