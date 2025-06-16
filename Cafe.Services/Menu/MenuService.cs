using Cafe.Repository.Repositories.Menu;
using Cafe.VM.ViewModels;

namespace Cafe.Services.Menu;

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
