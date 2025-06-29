using Cafe.Repository.Repositories.Generic;
using Cafe.Repository.Repositories.OrderType;
using Cafe.Services.Generic;
using Cafe.VM.HelperClasses;
using Cafe.VM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Services.OrderType
{
    public class OrderTypeService : IOrderTypeService
    {
        private readonly IOrderTypeRepo _repo;

        public OrderTypeService(IOrderTypeRepo repo)
        {
            _repo = repo;
        }

        public long Add(OrderTypeVm entity)
        {
            return _repo.Add(entity);
        }

        public bool Update(OrderTypeVm entity)
        {
            return _repo.Update(entity);
        }

        public bool Delete(long id, int userId)
        {
            return _repo.Delete(id, userId);
        }

        public OrderTypeVm Find(long id)
        {
            return _repo.Find(id);
        }

        public IList<OrderTypeVm> List()
        {
            return _repo.List();
        }

        public IList<OrderTypeVm> Search(string term)
        {
            return _repo.Search(term);
        }

        public bool ActivateEntity(long id, bool isActive, int userId)
        {
            return _repo.ActivateEntity(id, isActive, userId);
        }

        public bool CheckExist(OrderTypeVm entity)
        {
            return _repo.CheckExist(entity);
        }

        public IList<CustomOption> GetListDrop()
        {
            return _repo.GetListDrop();
        }
    }

}
