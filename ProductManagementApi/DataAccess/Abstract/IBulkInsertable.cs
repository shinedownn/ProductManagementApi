using ProductManagementApi.Entities;

namespace ProductManagementApi.DataAccess.Abstract
{
    public interface IBulkInsertable<T> where T : class, IEntity, new()
    {
        public Task<bool> BulkInsertAsync(IEnumerable<T> entities);
    }
}
