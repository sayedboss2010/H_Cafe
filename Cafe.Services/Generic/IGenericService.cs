using Cafe.VM.HelperClasses;

namespace Cafe.Services.Generic;

public interface IGenericService<TEntity>
{
    IList<TEntity> List();

    TEntity Find(long id);

    long Add(TEntity entity);

    bool Update(TEntity entity);

    bool Delete(long id, int userId);

    bool CheckExist(TEntity entity);

    IList<TEntity> Search(string term);
    IList<CustomOption> GetListDrop();

    bool ActivateEntity(long id, bool isActive, int userId);
}