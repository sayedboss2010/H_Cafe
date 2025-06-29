using Cafe.Repository.Repositories.EmployeeType;
using Cafe.Repository.Repositories.Generic;
using Cafe.Repository.Repositories.OrderType;
using Cafe.Services.Generic;
using Cafe.Services.OrderType;
using Cafe.VM.HelperClasses;
using Cafe.VM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Services.EmployeeType
{
    public class EmployeeTypeService : IEmployeeTypeService
    {
        private readonly IEmployeeTypeRepo _repo;

        public EmployeeTypeService(IEmployeeTypeRepo repo)
        {
            _repo = repo;
        }

        public long Add(EmployeeTypeVm entity)
        {
            return _repo.Add(entity);
        }

        public bool Update(EmployeeTypeVm entity)
        {
            return _repo.Update(entity);
        }

        public bool Delete(long id, int userId)
        {
            return _repo.Delete(id, userId);
        }

        public EmployeeTypeVm Find(long id)
        {
            return _repo.Find(id);
        }

        public IList<EmployeeTypeVm> List()
        {
            return _repo.List();
        }

        public IList<EmployeeTypeVm> Search(string term)
        {
            return _repo.Search(term);
        }

        public bool ActivateEntity(long id, bool isActive, int userId)
        {
            return _repo.ActivateEntity(id, isActive, userId);
        }

        public bool CheckExist(EmployeeTypeVm entity)
        {
            return _repo.CheckExist(entity);
        }

        public IList<CustomOption> GetListDrop()
        {
            return _repo.GetListDrop();
        }
    }

}
