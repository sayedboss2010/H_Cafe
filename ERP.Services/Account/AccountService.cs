using ERP.EF.Models;
using ERP.Repository.Repositories.Account;
using ERP.Repository.Repositories.Helper;
using ERP.VM;

namespace ERP.Services.Account;

public class AccountService : IAccountService
{
    private readonly IAccountRepo _account;
    private readonly IHelperRepo _helper;

    public AccountService(IAccountRepo account, IHelperRepo helper)
    {
        _account = account;
        _helper = helper;
    }

    public PrUserVm UserLogin(PrUserVm prUser)
    {
        var userVm = _account.UserLogin(prUser);

        if (userVm != null && !string.IsNullOrEmpty(userVm.UserName))
        {
            userVm.Sts = 1;
            userVm.AuthKey = _helper.HashMd5(userVm.Id.ToString() + userVm.UserTypeId.ToString());

            return userVm;
        }

        return new PrUserVm { Sts = 0 };
    }
}