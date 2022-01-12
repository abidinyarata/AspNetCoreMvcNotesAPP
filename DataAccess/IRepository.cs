using System.Collections.Generic;

namespace AspNetCoreMvcNotesAPP.DataAccess
{
    public interface IRepository<TEntity>
    {
        int Delete(int id);
        List<TEntity> GetAll();
        TEntity GetById(int id);
        TEntity Insert(TEntity entity);
        TEntity Update(int id, TEntity entity);
    }
}
