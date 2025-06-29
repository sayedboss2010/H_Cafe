using AutoMapper;
using Cafe.EF.Models;
using Cafe.Repository.Repositories.Generic;
using Cafe.Repository.Repositories.Helper;
using Cafe.VM.HelperClasses;
using Cafe.VM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafe.EF.Models;
namespace Cafe.Repository.Repositories.OrderType
{
    public class OrderTypeRepo : IOrderTypeRepo
    {
        private readonly IMapper _mapper;
        

        public OrderTypeRepo(IMapper mapper)
        {
            _mapper = mapper;
            
        }

        public long Add(OrderTypeVm entity)
        {
            //var obj = _mapper.Map<OrderType>(entity);
            var obj = _mapper.Map<OrderTypeVm, Cafe.EF.Models.OrderType>(entity);

            using var dbContext = new ErpDbContext();

          //  obj.OrderTypeID = int.Parse(_helper.GetSequencing("OrderType_SEQ", "int"));
            obj.IsActive = true;
            obj.IsDeleted = false;
            obj.CreationTime = DateTime.Now;

            dbContext.OrderTypes.Add(obj);
            dbContext.SaveChanges();

            return obj.OrderTypeID;
        }

        public bool Delete(long id, int userId)
        {
            using var dbContext = new ErpDbContext();
            var obj = dbContext.OrderTypes.Find((int)id);
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

        public bool Update(OrderTypeVm entity)
        {
            using var dbContext = new ErpDbContext();
            var obj = dbContext.OrderTypes.Find(entity.OrderTypeID);
            if (obj != null)
            {
                obj.NameEn = entity.NameEn;
                obj.NameAr = entity.NameAr;
                obj.UpdateUserID = entity.UpdateUserID;
                obj.UpdateTime = DateTime.Now;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public OrderTypeVm Find(long id)
        {
            using var dbContext = new ErpDbContext();
            var obj = dbContext.OrderTypes.Find((int)id);
            return obj == null ? new OrderTypeVm() : _mapper.Map<OrderTypeVm>(obj);
        }

        public IList<OrderTypeVm> List()
        {
            using var dbContext = new ErpDbContext();
            return _mapper.Map<List<OrderTypeVm>>(dbContext.OrderTypes.Where(o => !o.IsDeleted.Value).ToList());
        }

        public IList<OrderTypeVm> Search(string term)
        {
            using var dbContext = new ErpDbContext();
            return _mapper.Map<List<OrderTypeVm>>(
                dbContext.OrderTypes.Where(o => !o.IsDeleted.Value && (o.NameEn.Contains(term) || o.NameAr.Contains(term))).ToList()
            );
        }

        public bool ActivateEntity(long id, bool isActive, int userId)
        {
            using var dbContext = new ErpDbContext();
            var obj = dbContext.OrderTypes.Find((int)id);
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

        public bool CheckExist(OrderTypeVm entity)
        {
            using var dbContext = new ErpDbContext();
            return !dbContext.OrderTypes.Any(o =>
                !o.IsDeleted.Value && o.OrderTypeID != entity.OrderTypeID &&
                (o.NameAr == entity.NameAr || o.NameEn == entity.NameEn)
            );
        }

        public IList<CustomOption> GetListDrop()
        {
            using var dbContext = new ErpDbContext();
            return dbContext.OrderTypes.Where(o => o.IsActive == true && o.IsDeleted == false)
                .Select(o => new CustomOption { Id = o.OrderTypeID, NameAr = o.NameAr }).ToList();
        }
    }

}
