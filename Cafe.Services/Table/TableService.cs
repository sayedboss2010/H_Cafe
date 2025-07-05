using Cafe.Repository.Repositories.OrderType;
using Cafe.Repository.Repositories.Table;
using Cafe.VM.HelperClasses;
using Cafe.VM.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Services.Table
{
    public class TableService: ITableService
    {
        private readonly ITableRepo _repo;

        public TableService(ITableRepo repo)
        {
            _repo = repo;
        }

        public long Add(TableVm entity)
        {
            return _repo.Add(entity);
        }

        public bool Update(TableVm entity)
        {
            return _repo.Update(entity);
        }

        public bool Delete(long id, int userId)
        {
            return _repo.Delete(id, userId);
        }

        public TableVm Find(long id)
        {
            return _repo.Find(id);
        }

        public IList<TableVm> List()
        {
            return _repo.List();
        }
        public IList<TableVm> ListByEntityID(int EntityID)
        {
            return _repo.ListByEntityID(EntityID);
        }

        public IList<TableVm> Search(string term)
        {
            return _repo.Search(term);
        }

        public bool ActivateEntity(long id, bool isActive, int userId)
        {
            return _repo.ActivateEntity(id, isActive, userId);
        }

        public bool CheckExist(TableVm entity)
        {
            return _repo.CheckExist(entity);
        }

        public IList<CustomOption> GetListDrop()
        {
            return _repo.GetListDrop();
        }
    }
}
