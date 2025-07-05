using Cafe.VM.HelperClasses;
using Cafe.VM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Services.Location
{
    public interface ILocationSerivce
    {
        public long Add(LocationCafeVm entity);


        public bool Update(LocationCafeVm entity);


        public bool Delete(long id, int userId);


        public LocationCafeVm Find(long id);


        public IList<LocationCafeVm> List();

        public IList<LocationCafeVm> Search(string term);


        public bool ActivateEntity(long id, bool isActive, int userId);


        public bool CheckExist(LocationCafeVm entity);

        public IList<CustomOption> GetListDrop();
        public IList<CustomOption> GetListDropByEntityID(int EnitiyID);
    }
}
