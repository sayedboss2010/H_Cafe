using Cafe.VM.ViewModels;

namespace Cafe.Services.Menu;

public interface IMenuService
{
    List<DrawMenuResulVm> DrawMenu(int userId);
}