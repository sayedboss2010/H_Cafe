using AutoMapper;
using Cafe.EF.Models;
using Cafe.VM.HelperClasses;
using Cafe.VM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Repository.Repositories.Employee
{
    public class EmployeeRepo:IEmployeeRepo
    {
        private readonly IMapper _mapper;


        public EmployeeRepo(IMapper mapper)
        {
            _mapper = mapper;

        }

        public long Add(EmployeeCafeVm entity)
        {
            //var obj = _mapper.Map<EmployeeType>(entity);
            var obj = _mapper.Map<EmployeeCafeVm, Cafe.EF.Models.Employee>(entity);

            using var dbContext = new ErpDbContext();

            //  obj.OrderTypeID = int.Parse(_helper.GetSequencing("OrderType_SEQ", "int"));
            obj.IsActive = true;
            obj.IsDeleted = false;
            obj.CreationTime = DateTime.Now;

            dbContext.Employees.Add(obj);
            dbContext.SaveChanges();

            return obj.EmployeeID;
        }

        public bool Delete(long id, int userId)
        {
            using var dbContext = new ErpDbContext();
            var obj = dbContext.Employees.Find((int)id);
            if (obj != null)
            {
                obj.IsActive = false;
                obj.IsDeleted = true;
                obj.DeletionTime = DateTime.Now;
                obj.DeletionUserID = userId;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Update(EmployeeCafeVm entity)
        {
            using var dbContext = new ErpDbContext();
            var obj = dbContext.Employees.Find(entity.EmployeeID);
            if (obj != null)
            {
                obj.NameEn = entity.NameEn;
                obj.NameAr = entity.NameAr;
                obj.Phone = entity.Phone;
                obj.EmployeeTypeID = entity.EmployeeTypeID;

                obj.UpdateUserID = entity.UpdateUserID;
                obj.UpdateTime = DateTime.Now;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public EmployeeCafeVm Find(long id)
        {
            using var dbContext = new ErpDbContext();
            var obj = dbContext.Employees.Find((int)id);
            return obj == null ? new EmployeeCafeVm() : _mapper.Map<EmployeeCafeVm>(obj);
        }

        public IList<EmployeeCafeVm> List()
        {
            using var dbContext = new ErpDbContext();
            return _mapper.Map<List<EmployeeCafeVm>>(dbContext.Employees.Where(o => !o.IsDeleted.Value).ToList());
        }

        public IList<EmployeeCafeVm> Search(string term)
        {
            using var dbContext = new ErpDbContext();
            return _mapper.Map<List<EmployeeCafeVm>>(
                dbContext.Employees.Where(o => !o.IsDeleted.Value && (o.NameEn.Contains(term) || o.NameAr.Contains(term))).ToList()
            );
        }

        public bool ActivateEntity(long id, bool isActive, int userId)
        {
            using var dbContext = new ErpDbContext();
            var obj = dbContext.Employees.Find((int)id);
            if (obj != null)
            {
                obj.IsActive = isActive;
                obj.UpdateUserID = userId;
                obj.UpdateTime = DateTime.Now;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool CheckExist(EmployeeCafeVm entity)
        {
            using var dbContext = new ErpDbContext();
            return !dbContext.Employees.Any(o =>
                !o.IsDeleted.Value && o.EmployeeID != entity.EmployeeID &&
                (o.NameAr == entity.NameAr || o.NameEn == entity.NameEn)
            );
        }

        public IList<CustomOption> GetListDrop()
        {
            using var dbContext = new ErpDbContext();
            return dbContext.Employees.Where(o => o.IsActive == true && o.IsDeleted == false)
                .Select(o => new CustomOption { Id = o.EmployeeID, NameAr = o.NameAr }).ToList();
        }

    }
}
