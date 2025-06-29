using Cafe.Repository.Repositories.Employee;
using Cafe.Repository.Repositories.EmployeeType;
using Cafe.VM.HelperClasses;
using Cafe.VM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Services.Employee
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepo _repo;

        public EmployeeService(IEmployeeRepo repo)
        {
            _repo = repo;
        }

        public long Add(EmployeeCafeVm entity)
        {
            return _repo.Add(entity);
        }

        public bool Update(EmployeeCafeVm entity)
        {
            return _repo.Update(entity);
        }

        public bool Delete(long id, int userId)
        {
            return _repo.Delete(id, userId);
        }

        public EmployeeCafeVm Find(long id)
        {
            return _repo.Find(id);
        }

        public IList<EmployeeCafeVm> List()
        {
            return _repo.List();
        }

        public IList<EmployeeCafeVm> Search(string term)
        {
            return _repo.Search(term);
        }

        public bool ActivateEntity(long id, bool isActive, int userId)
        {
            return _repo.ActivateEntity(id, isActive, userId);
        }

        public bool CheckExist(EmployeeCafeVm entity)
        {
            return _repo.CheckExist(entity);
        }

        public IList<CustomOption> GetListDrop()
        {
            return _repo.GetListDrop();
        }
    }
}
