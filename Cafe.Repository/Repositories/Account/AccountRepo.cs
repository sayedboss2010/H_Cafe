using AutoMapper;
using Cafe.EF.Models;
using Cafe.Repository.Repositories.Helper;
using Cafe.VM;

namespace Cafe.Repository.Repositories.Account;

public class AccountRepo : IAccountRepo
{
    private readonly IHelperRepo _helper;
    private readonly IMapper _mapper;
    public AccountRepo(IHelperRepo helper, IMapper mapper)
    {
        _helper = helper;
        _mapper = mapper;
    }

    public PrUserVm UserLogin(PrUserVm prUser)
    {
        using var dbContext = new ErpDbContext();

        var user = dbContext.PR_Users
            .FirstOrDefault(u => u.UserName == prUser.UserName && u.Password == _helper.HashMd5(prUser.Password));

        return _mapper.Map<PrUserVm>(user);
        //return null;
    }
}