using Microsoft.Extensions.Options;
using ProductManagementApi.Entities.Concrete;

namespace ProductManagementApi.DataAccess.Abstract
{
    public abstract class IUserRepository : IBaseRepository<UserEntity>
    {
        protected IUserRepository(IOptions<AppSettingsModel> appSettings) : base(appSettings)
        {
        }
    }
}
