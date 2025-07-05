using Cafe.VM.HelperClasses;
using Cafe.VM.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Repository.Repositories.Table
{
    public interface ITableRepo
    {
        public long Add(TableVm entity);

        public bool Delete(long id, int userId);


        public bool Update(TableVm entity);

        public TableVm Find(long id);


        public IList<TableVm> List();

        public IList<TableVm> ListByEntityID(int EntityID);
        public IList<TableVm> Search(string term);


        public bool ActivateEntity(long id, bool isActive, int userId);

        public bool CheckExist(TableVm entity);


        public IList<CustomOption> GetListDrop();
    }
}
