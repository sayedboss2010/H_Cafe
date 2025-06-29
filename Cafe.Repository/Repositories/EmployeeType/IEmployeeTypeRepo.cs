using AutoMapper;
using Cafe.EF.Models;
using Cafe.Repository.Repositories.Helper;
using Cafe.VM.HelperClasses;
using Cafe.VM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Repository.Repositories.EmployeeType
{
    public interface IEmployeeTypeRepo
    {



        public long Add(EmployeeTypeVm entity);

        public bool Delete(long id, int userId);


        public bool Update(EmployeeTypeVm entity);

        public EmployeeTypeVm Find(long id);


        public IList<EmployeeTypeVm> List();


        public IList<EmployeeTypeVm> Search(string term);


        public bool ActivateEntity(long id, bool isActive, int userId);

        public bool CheckExist(EmployeeTypeVm entity);


        public IList<CustomOption> GetListDrop();
        
    }
}
