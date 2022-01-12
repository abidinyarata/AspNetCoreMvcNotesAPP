using System.Collections.Generic;

namespace AspNetCoreMvcNotesAPP.Business
{
    public interface IServiceOperations<TEntity, TCreateViewModel, TEditViewModel>
    {
        ServiceResult<TEntity> Create(TCreateViewModel model);
        ServiceResult<TEntity> Find(int id);
        ServiceResult<List<TEntity>> ListAll();
        ServiceResult<object> Remove(int id);
        ServiceResult<TEntity> Update(int id, TEditViewModel model);
    }
}
