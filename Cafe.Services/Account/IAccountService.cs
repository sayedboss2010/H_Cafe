using Cafe.VM;

namespace Cafe.Services.Account;

public interface IAccountService
{
    PrUserVm UserLogin(PrUserVm prUser);
}