using Cafe.VM.HelperClasses;
using Cafe.VM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Services.Employee
{
    public interface IEmployeeService
    {

        public long Add(EmployeeCafeVm entity);


        public bool Update(EmployeeCafeVm entity);


        public bool Delete(long id, int userId);


        public EmployeeCafeVm Find(long id);


        public IList<EmployeeCafeVm> List();

        public IList<EmployeeCafeVm> Search(string term);


        public bool ActivateEntity(long id, bool isActive, int userId);


        public bool CheckExist(EmployeeCafeVm entity);

        public IList<CustomOption> GetListDrop();

    }
}
