using Cafe.VM.ViewModels;

namespace Cafe.Repository.Repositories.Menu;

public interface IMenuRepo
{
    List<DrawMenuResulVm> DrawMenu(int userId);
}