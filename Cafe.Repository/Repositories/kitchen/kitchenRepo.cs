using Cafe.EF.Models;
using Cafe.Repository.Repositories.Generic;
using Cafe.VM.HelperClasses;
using Cafe.VM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.Marshalling.IIUnknownCacheStrategy;

namespace Cafe.Repository.Repositories.kitchen
{
    public class kitchenRepo : IGenericRepo<kitchenVM>
    {
        public bool ActivateEntity(long id, bool isActive, int userId)
        {
            throw new NotImplementedException();
        }

        public long Add(kitchenVM entity)
        {
            throw new NotImplementedException();
        }

        public bool CheckExist(kitchenVM entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(long id, int userId)
        {
            throw new NotImplementedException();
        }

        public kitchenVM Find(long id)
        {
            throw new NotImplementedException();
        }

        public IList<CustomOption> GetListDrop()
        {
            throw new NotImplementedException();
        }

        public IList<kitchenVM> List()
        {

            throw new NotImplementedException();

        }

        public IList<kitchenVM> Search(string term)
        {

            try
            {
                int EntityId = int.Parse(term);
                using var dbContext = new ErpDbContext();
                List<kitchenVM> data= new List<kitchenVM>();

                data = dbContext.Orders
     .Where(o => o.ISDone==null  && o.IsDeleted==false &&o.Tabel.Location.EntityID== EntityId)
     .Select(o =>  new kitchenVM
     {
         OrderID=o.OrderID,
         OrderTypeID=o.OrderTypeID,
         OrderTypeNameEn = o.OrderType.NameEn,
         OrderTypeNameAr = o.OrderType.NameAr,
         TableID=o.TabelID,

         LocationID = o.Tabel.LocationID,

         LocationNameAr = o.Tabel.Location.NameAr,
         LocationNameEn = o.Tabel.Location.NameEn,


         OrderDate=o.OrderDate.ToString(),
         Notes = o.Notes,

         OrderDetails = o.OrderDetails
             .Where(d => d.IsDeleted==false)
             .Select(d => new kitchenOrderDetailVm
             {
                 OrderDetailID=d.OrderDetailID,
                 ItemID=d.ItemID,
                 ItemNameAr=d.Item.NameAr,
                 ItemNameEn = d.Item.NameEn,
                 Quantity=d.Quantity,
             }).ToList()
     }).ToList();



                return data;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool Update(kitchenVM entity)
        {
            try
            {
                using var dbContext = new ErpDbContext();
                var dataorder = dbContext.Orders.Find(entity.OrderID);
                if (dataorder!=null)
                {
                    dataorder.ISDone = true;
                    dataorder.UpdateTime = DateTime.Now;

                }
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
