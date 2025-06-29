using Cafe.Repository.Repositories.Generic;
using Cafe.VM.HelperClasses;
using Cafe.VM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Services.OrderType
{
    public interface IEmployeeTypeService
    {



        public long Add(EmployeeTypeVm entity);


        public bool Update(EmployeeTypeVm entity);


        public bool Delete(long id, int userId);


        public EmployeeTypeVm Find(long id);


        public IList<EmployeeTypeVm> List();

        public IList<EmployeeTypeVm> Search(string term);


        public bool ActivateEntity(long id, bool isActive, int userId);


        public bool CheckExist(EmployeeTypeVm entity);

        public IList<CustomOption> GetListDrop();
        
    }
}
