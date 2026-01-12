using Microsoft.Extensions.Options;
using ProductManagementApi.Entities.Concrete;

namespace ProductManagementApi.DataAccess.Abstract
{
    public abstract class IProductRepository : IBaseRepository<ProductEntity>,IBulkInsertable<ProductEntity>
    {
        protected IProductRepository(IOptions<AppSettingsModel> appSettings) : base(appSettings)
        {
        }

        public abstract Task<bool> BulkInsertAsync(IEnumerable<ProductEntity> entities);
    }
}
