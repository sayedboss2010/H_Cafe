using AutoMapper;
using ERP.EF.Models;
using ERP.VM.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ERP.Repository.Repositories.Menu;

public class MenuRepo : IMenuRepo
{
    private readonly IMapper _mapper;

    public MenuRepo(IMapper mapper)
    {
        _mapper = mapper;
    }

    public List<DrawMenuResulVm> DrawMenu(int userId)
    {
        using var dbContext = new ErpDbContext();
        var lst = dbContext.DrawMenuResul
            .FromSqlRaw("Get_Group_Mnue_ByUser {0}, {1}", userId, 1).ToList();
        return _mapper.Map<List<DrawMenuResulVm>>(lst);
    }
}