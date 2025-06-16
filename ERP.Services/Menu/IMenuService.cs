using ERP.VM.ViewModels;

namespace ERP.Services.Menu;

public interface IMenuService
{
    List<DrawMenuResulVm> DrawMenu(int userId);
}