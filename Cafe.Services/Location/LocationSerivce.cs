using Cafe.Repository.Repositories.Location;
using Cafe.Repository.Repositories.Table;
using Cafe.VM.HelperClasses;
using Cafe.VM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Services.Location
{
    public class LocationSerivce : ILocationSerivce
    {
        private readonly ILocationRepo _repo;

        public LocationSerivce(ILocationRepo repo)
        {
            _repo = repo;
        }

        public long Add(LocationCafeVm entity)
        {
            return _repo.Add(entity);
        }

        public bool Update(LocationCafeVm entity)
        {
            return _repo.Update(entity);
        }

        public bool Delete(long id, int userId)
        {
            return _repo.Delete(id, userId);
        }

        public LocationCafeVm Find(long id)
        {
            return _repo.Find(id);
        }

        public IList<LocationCafeVm> List()
        {
            return _repo.List();
        }

        public IList<LocationCafeVm> Search(string term)
        {
            return _repo.Search(term);
        }

        public bool ActivateEntity(long id, bool isActive, int userId)
        {
            return _repo.ActivateEntity(id, isActive, userId);
        }

        public bool CheckExist(LocationCafeVm entity)
        {
            return _repo.CheckExist(entity);
        }

        public IList<CustomOption> GetListDrop()
        {
            return _repo.GetListDrop();
        }

        
        IList<CustomOption> ILocationSerivce.GetListDropByEntityID(int EnitiyID)
        {
            return _repo.GetListDropByEntityID(EnitiyID);
        }
    }
}
