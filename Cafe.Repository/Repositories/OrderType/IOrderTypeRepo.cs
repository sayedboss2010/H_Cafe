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

namespace Cafe.Repository.Repositories.OrderType
{
    public interface IOrderTypeRepo
    {



        public long Add(OrderTypeVm entity);

        public bool Delete(long id, int userId);


        public bool Update(OrderTypeVm entity);

        public OrderTypeVm Find(long id);


        public IList<OrderTypeVm> List();


        public IList<OrderTypeVm> Search(string term);


        public bool ActivateEntity(long id, bool isActive, int userId);

        public bool CheckExist(OrderTypeVm entity);


        public IList<CustomOption> GetListDrop();
        
    }
}
