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
    public interface IOrderTypeService
    {



        public long Add(OrderTypeVm entity);


        public bool Update(OrderTypeVm entity);


        public bool Delete(long id, int userId);


        public OrderTypeVm Find(long id);


        public IList<OrderTypeVm> List();

        public IList<OrderTypeVm> Search(string term);


        public bool ActivateEntity(long id, bool isActive, int userId);


        public bool CheckExist(OrderTypeVm entity);

        public IList<CustomOption> GetListDrop();
        
    }
}
