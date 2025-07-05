using AutoMapper;
using Cafe.EF.Models;
using Cafe.VM.HelperClasses;
using Cafe.VM.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Repository.Repositories.Table
{
    public class TableRepo : ITableRepo
    {
        private readonly IMapper _mapper;
        public TableRepo(IMapper mapper)
        {
            _mapper = mapper;
        }


        public long Add(TableVm entity)
        {
            //var obj = _mapper.Map<OrderType>(entity);
            var obj = _mapper.Map<TableVm, Cafe.EF.Models.Table>(entity);

            using var dbContext = new ErpDbContext();

            //  obj.TableID = int.Parse(_helper.GetSequencing("OrderType_SEQ", "int"));
            obj.IsActive = true;
            obj.IsDeleted = false;
            obj.CreationTime = DateTime.Now;

            dbContext.Tables.Add(obj);
            dbContext.SaveChanges();

            obj.QRCode = "&locationID=" + entity.LocationID + "&TableID=" + obj.TabelID;
            dbContext.SaveChanges();

            return obj.TabelID;
        }

        public bool Delete(long id, int userId)
        {
            using var dbContext = new ErpDbContext();
            var obj = dbContext.Tables.Find((int)id);
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

        public bool Update(TableVm entity)
        {
            using var dbContext = new ErpDbContext();
            var obj = dbContext.Tables.Find(entity.TabelID);
            if (obj != null)
            {
                obj.LocationID = entity.LocationID;
                obj.Notes = entity.Notes;
                obj.QRCode = "&locationID=" + entity.LocationID + "&TableID=" + obj.TabelID;
                obj.UpdateUserID = entity.UpdateUserID;
                obj.UpdateTime = DateTime.Now;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public TableVm Find(long id)
        {
            using var dbContext = new ErpDbContext();
            var obj = dbContext.Tables.Find((int)id);
            return obj == null ? new TableVm() : _mapper.Map<TableVm>(obj);
        }

        public IList<TableVm> List()
        {
            using var dbContext = new ErpDbContext();
            return _mapper.Map<List<TableVm>>(dbContext.Tables.Where(o => !o.IsDeleted.Value).ToList());
        }
        public IList<TableVm> ListByEntityID(int EntityID)
        {
            using var dbContext = new ErpDbContext();
            return _mapper.Map<List<TableVm>>(dbContext.Tables.Include(o => o.Location).Where(o => !o.IsDeleted.Value&& o.Location.EntityID.Value== EntityID).ToList());
        }

        
        public IList<TableVm> Search(string term)
        {
            using var dbContext = new ErpDbContext();
            return _mapper.Map<List<TableVm>>(
                dbContext.Tables.Where(o => !o.IsDeleted.Value && (o.QRCode.Contains(term) || o.QRCode.Contains(term)||o.Location.NameAr.Contains(term)|| o.Location.NameEn.Contains(term))).ToList()
            );
        }

        public bool ActivateEntity(long id, bool isActive, int userId)
        {
            using var dbContext = new ErpDbContext();
            var obj = dbContext.Tables.Find((int)id);
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

        public bool CheckExist(TableVm entity)
        {
            using var dbContext = new ErpDbContext();
            return !dbContext.Tables.Any(o =>
                !o.IsDeleted.Value && o.TabelID != entity.TabelID 
            );
        }

        public IList<CustomOption> GetListDrop()
        {
            using var dbContext = new ErpDbContext();
            return dbContext.Tables.Where(o => o.IsActive == true && o.IsDeleted == false)
                .Select(o => new CustomOption { Id = o.TabelID, NameAr = o.Notes }).ToList();
        }
    }
}
