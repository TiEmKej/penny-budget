using System.Threading.Tasks;

namespace PennyBudget.ViewModels;

public interface IRefreshable
{
    Task Load();
}
