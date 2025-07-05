using AutoMapper;
using Cafe.EF.Models;
using Cafe.VM.HelperClasses;
using Cafe.VM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Repository.Repositories.Location
{
    public class LocationRepo : ILocationRepo
    {
        private readonly IMapper _mapper;
        public LocationRepo(IMapper mapper)
        {
            _mapper = mapper;
        }


        public long Add(LocationCafeVm entity)
        {
            //var obj = _mapper.Map<OrderType>(entity);
            var obj = _mapper.Map<LocationCafeVm, Cafe.EF.Models.Location>(entity);

            using var dbContext = new ErpDbContext();

            //  obj.LocationID = int.Parse(_helper.GetSequencing("OrderType_SEQ", "int"));
            obj.IsActive = true;
            obj.IsDeleted = false;
            obj.CreationTime = DateTime.Now;

            dbContext.Locations.Add(obj);
            dbContext.SaveChanges();

            return obj.LocationID;
        }

        public bool Delete(long id, int userId)
        {
            using var dbContext = new ErpDbContext();
            var obj = dbContext.Locations.Find((int)id);
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

        public bool Update(LocationCafeVm entity)
        {
            using var dbContext = new ErpDbContext();
            var obj = dbContext.Locations.Find(entity.LocationID);
            if (obj != null)
            {
                //obj.NameEn = entity.NameEn;
                //obj.NameAr = entity.NameAr;
                obj.UpdateUserID = entity.UpdateUserID;
                obj.UpdateTime = DateTime.Now;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public LocationCafeVm Find(long id)
        {
            using var dbContext = new ErpDbContext();
            var obj = dbContext.Locations.Find((int)id);
            return obj == null ? new LocationCafeVm() : _mapper.Map<LocationCafeVm>(obj);
        }

        public IList<LocationCafeVm> List()
        {
            using var dbContext = new ErpDbContext();
            return _mapper.Map<List<LocationCafeVm>>(dbContext.Locations.Where(o => !o.IsDeleted.Value).ToList());
        }

        public IList<LocationCafeVm> Search(string term)
        {
            using var dbContext = new ErpDbContext();
            return _mapper.Map<List<LocationCafeVm>>(
                dbContext.Locations.Where(o => !o.IsDeleted.Value && (o.NameAr.Contains(term) || o.NameEn.Contains(term) )).ToList()
            );
        }

        public bool ActivateEntity(long id, bool isActive, int userId)
        {
            using var dbContext = new ErpDbContext();
            var obj = dbContext.Locations.Find((int)id);
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

        public bool CheckExist(LocationCafeVm entity)
        {
            using var dbContext = new ErpDbContext();
            return !dbContext.Locations.Any(o =>
                !o.IsDeleted.Value && o.LocationID != entity.LocationID &&
                (o.NameAr == entity.NameAr)
            );
        }

        public IList<CustomOption> GetListDrop()
        {
            using var dbContext = new ErpDbContext();
            return dbContext.Locations.Where(o => o.IsActive == true && o.IsDeleted == false)
                .Select(o => new CustomOption { Id = o.LocationID, NameAr = o.NameAr }).ToList();
        }

        public IList<CustomOption> GetListDropByEntityID(int EnitiyID)
        {
            using var dbContext = new ErpDbContext();
            return dbContext.Locations.Where(o => o.IsActive == true && o.IsDeleted == false&&o.EntityID== EnitiyID)
                .Select(o => new CustomOption { Id = o.LocationID, NameAr = o.NameAr }).ToList();
        }
    }
}
