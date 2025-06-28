using Cafe.EF.Models;
using Cafe.Repository.Repositories.Generic;
using Cafe.Repository.Repositories.MenuCafe;
using Cafe.Services.Generic;
using Cafe.VM.HelperClasses;
using Cafe.VM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Services.kitchen
{
    public class kitchenService :IGenericService<kitchenVM>
    {

        private readonly IGenericRepo<kitchenVM> _order;
        public kitchenService(IGenericRepo<kitchenVM> order)
        {
            _order = order;
        }

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
            return  _order.Search(term);
        }

        public bool Update(kitchenVM entity)
        {
            return _order.Update(entity);
        }
    }
}
