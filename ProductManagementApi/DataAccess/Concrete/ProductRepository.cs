using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using ProductManagementApi.DataAccess.Abstract;
using ProductManagementApi.Entities.Concrete;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;


namespace ProductManagementApi.DataAccess.Concrete
{
    public class ProductRepository : IProductRepository, IBulkInsertable<ProductEntity>
    {
        public ProductRepository(IOptions<AppSettingsModel> appSettings) : base(appSettings)
        {
        }

        public override async Task<bool> BulkInsertAsync(IEnumerable<ProductEntity> entityList)
        {
            var result = false;
            using (SqlConnection db = CreateConnection())
            {
                try
                {
                    db.Open();

                    var d = entityList.Select(x => x with
                    {
                        CreatedAt = DateTime.Now,
                        IsActive = true
                    });
                    
                    await Dapper.Bulk.DapperBulk.BulkInsertAsync<ProductEntity>(db,d);
                    db.Close();
                    result= true;
                }
                catch (Exception ex)
                {
                    result = false;
                }
                finally { db.Close(); }     
                return result;
            }
        }

        public override async Task<bool> CreateAsync(ProductEntity entity)
        {
            using (IDbConnection db = CreateConnection())
            {
                var list = entity.GetType().GetProperties()
                    .Where(p => p.CanRead &&
                p.Name != "EqualityContract" && p.GetCustomAttribute<KeyAttribute>() == null && p.GetValue(entity) != null)
                    .ToList();
                Dictionary<string, object> spParams = new Dictionary<string, object>();

                list.ForEach(x =>
                {
                    spParams[x.Name] = x.GetValue(entity);
                });
                var result = await db.ExecuteAsync($"sp_Product_Create", spParams, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            using (IDbConnection db = CreateConnection())
            {                 
                int result = await db.ExecuteAsync($"sp_Product_Delete", new { Id=id }, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public override async Task<ProductEntity> GetAsync(int id)
        {
            using (IDbConnection db = CreateConnection())
            {             
                return await db.QueryFirstOrDefaultAsync<ProductEntity>($"sp_Product_Get", new {Id=id}, commandType: CommandType.StoredProcedure);
            }
        }

        public override async Task<ProductEntity> GetAsync(ProductEntity where)
        {
            using (IDbConnection db = CreateConnection())
            {
                var list = where.GetType().GetProperties()
                    .Where(p =>p.CanRead &&
                p.Name != "EqualityContract" && p.GetValue(where) != null)
                    .ToList();
                Dictionary<string, object> spParams = new Dictionary<string, object>();

                list.ForEach(x =>
                {
                    spParams[x.Name] = x.GetValue(where);
                });
                return await db.QueryFirstOrDefaultAsync<ProductEntity>($"sp_Product_Get", spParams, commandType: CommandType.StoredProcedure);
            }
        }

        public override async Task<IEnumerable<ProductEntity>> GetListAsync()
        {
            using (IDbConnection db = CreateConnection())
            {
                return await db.QueryAsync<ProductEntity>($"sp_Product_GetList", commandType: CommandType.StoredProcedure);                 
            }
        }

        public override async Task<bool> UpdateAsync(ProductEntity entity)
        {
            using (IDbConnection db = CreateConnection())
            {
                var list = entity.GetType().GetProperties()
                    .Where(p => p.CanRead &&
                p.Name != "EqualityContract" && p.GetValue(entity) != null)
                    .ToList();
                Dictionary<string, object> spParams = new Dictionary<string, object>();

                list.ForEach(x =>
                {
                    spParams[x.Name] = x.GetValue(entity);
                });

                int result = await db.ExecuteAsync($"sp_Product_Update", spParams, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }
    }
}
