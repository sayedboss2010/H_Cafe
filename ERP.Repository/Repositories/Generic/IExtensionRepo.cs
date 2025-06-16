namespace ERP.Repository.Repositories.Generic;

public interface IExtensionRepo<TEntity>
{
    IList<TEntity> GetListByParent(TEntity entity);

    void RemoveAllChild(TEntity entity);
}