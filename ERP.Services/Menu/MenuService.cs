using ERP.Repository.Repositories.Menu;
using ERP.VM.ViewModels;

namespace ERP.Services.Menu;

public class MenuService : IMenuService
{
    private readonly IMenuRepo _menuRepo;
    public MenuService(IMenuRepo menuRepo)
    {
        _menuRepo = menuRepo;
    }
    public List<DrawMenuResulVm> DrawMenu(int userId)
    {
        return _menuRepo.DrawMenu(userId);
    }
}
