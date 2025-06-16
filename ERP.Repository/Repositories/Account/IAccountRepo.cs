using ERP.VM;
using ERP.VM.ViewModels;

namespace ERP.Repository.Repositories.Account;

public interface IAccountRepo
{
    PrUserVm UserLogin(PrUserVm prUser);
}