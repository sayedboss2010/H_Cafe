using ERP.VM.ViewModels;

namespace ERP.Repository.Repositories.Menu;

public interface IMenuRepo
{
    List<DrawMenuResulVm> DrawMenu(int userId);
}