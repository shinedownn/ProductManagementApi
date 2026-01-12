using Dapper;
using Microsoft.Extensions.Options;
using ProductManagementApi.DataAccess.Abstract;
using ProductManagementApi.Entities.Concrete;
using System.Data;

namespace ProductManagementApi.DataAccess.Concrete
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(IOptions<AppSettingsModel> appSettings) : base(appSettings)
        {
        }

        public override async Task<bool> CreateAsync(UserEntity entity)
        {
            using (IDbConnection db = CreateConnection())
            {
                var list = entity.GetType().GetProperties()
                    .Where(p => p.GetValue(entity) != null)
                    .ToList();
                Dictionary<string, object> spParams = new Dictionary<string, object>();

                list.ForEach(x =>
                {
                    spParams[x.Name] = x.GetValue(entity);
                });
                var result = await db.ExecuteAsync($"sp_User_Create", spParams, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            using (IDbConnection db = CreateConnection())
            {
                int result = await db.ExecuteAsync($"sp_User_Delete", new { Id = id }, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public override async Task<UserEntity> GetAsync(int id)
        {
            using (IDbConnection db = CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<UserEntity>($"sp_User_Get", new { Id = id }, commandType: CommandType.StoredProcedure);
            }
        }

        public override async Task<UserEntity> GetAsync(UserEntity where)
        {
            using (IDbConnection db = CreateConnection())
            {
                var list = where.GetType().GetProperties()
                    .Where(p => p.GetValue(where) != null)
                    .ToList();
                Dictionary<string, object> spParams = new Dictionary<string, object>();

                list.ForEach(x =>
                {
                    spParams[x.Name] = x.GetValue(where);
                });
                return await db.QueryFirstOrDefaultAsync<UserEntity>($"sp_User_Get", spParams , commandType: CommandType.StoredProcedure);
            }
        }

        public override async Task<IEnumerable<UserEntity>> GetListAsync()
        {
            using (IDbConnection db = CreateConnection())
            {
                return await db.QueryAsync<UserEntity>($"sp_User_GetList", commandType: CommandType.StoredProcedure);
            }
        }

        public override async Task<bool> UpdateAsync(UserEntity entity)
        {
            using (IDbConnection db = CreateConnection())
            {
                var list = entity.GetType().GetProperties()
                    .Where(p => p.GetValue(entity) != null)
                    .ToList();
                Dictionary<string, object> spParams = new Dictionary<string, object>();

                list.ForEach(x =>
                {
                    spParams[x.Name] = x.GetValue(entity);
                });

                int result = await db.ExecuteAsync($"sp_User_Update", spParams, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }
    }
}
