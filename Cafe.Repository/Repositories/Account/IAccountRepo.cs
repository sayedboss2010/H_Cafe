using Cafe.VM;
using Cafe.VM.ViewModels;

namespace Cafe.Repository.Repositories.Account;

public interface IAccountRepo
{
    PrUserVm UserLogin(PrUserVm prUser);
}