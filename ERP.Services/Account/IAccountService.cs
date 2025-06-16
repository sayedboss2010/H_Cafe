using ERP.VM;

namespace ERP.Services.Account;

public interface IAccountService
{
    PrUserVm UserLogin(PrUserVm prUser);
}