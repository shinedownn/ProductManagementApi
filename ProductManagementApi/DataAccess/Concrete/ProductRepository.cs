using AutoMapper;
using Dapper;
using ProductManagementApi.DataAccess.Abstract;
using ProductManagementApi.Entities.Concrete;
using ProductManagementApi.Entities.Dtos.ProductDto;
using ProductManagementApi.Entities.EndpointParams.Product;
using System.Data;
using Z.Dapper.Plus;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProductManagementApi.DataAccess.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        public ProductRepository(IConfiguration configuration, IMapper mapper) : base(configuration)
        {
            _mapper = mapper;
        }

        public override async Task<bool> BulkInsert(IEnumerable<CreateProductParams> paramList)
        {
            using (IDbConnection db = CreateConnection())
            {
                try
                {
                    DapperPlusManager
                .Entity<ProductEntity>()
                .Table("Products").Identity(x => x.Id); ;
                    var d = paramList.Select(x => new ProductEntity()
                    {
                        Category = x.Category,
                        CreatedAt = DateTime.Now,
                        IsActive = true,
                        Name = x.Name,
                        Price = x.Price
                    });
                    var result = await db.BulkInsertAsync(d);
                    return true;
                }
                catch (Exception ex)
                {
                    return false; 
                } 
            }
        }

        public override async Task<bool> CreateAsync(CreateProductParams param)
        {
            using (IDbConnection db = CreateConnection())
            {
                var list = param.GetType().GetProperties()
                    .Where(p => p.GetValue(param) != null)
                    .ToList();
                Dictionary<string, object> spParams = new Dictionary<string, object>();

                list.ForEach(x =>
                {
                    spParams[x.Name] = x.GetValue(param);
                });
                var result = await db.ExecuteAsync($"sp_Product_Create", spParams, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public override async Task<bool> DeleteAsync(DeleteProductParams param)
        {
            using (IDbConnection db = CreateConnection())
            {
                var list = param.GetType().GetProperties()
                    .Where(p => p.GetValue(param) != null)
                    .ToList();
                Dictionary<string, object> spParams = new Dictionary<string, object>();

                list.ForEach(x =>
                {
                    spParams[x.Name] = x.GetValue(param);
                });
                int result = await db.ExecuteAsync($"sp_Product_Delete", spParams, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public override async Task<GetProductDto> GetAsync(GetProductParams param)
        {
            using (IDbConnection db = CreateConnection())
            {
                var list = param.GetType().GetProperties()
                    .Where(p => p.GetValue(param) != null)
                    .ToList();
                Dictionary<string, object> spParams = new Dictionary<string, object>();

                list.ForEach(x =>
                {
                    spParams[x.Name] = x.GetValue(param);
                });

                return _mapper.Map<GetProductDto>(await db.QueryFirstOrDefaultAsync<ProductEntity>($"sp_Product_Get", spParams, commandType: CommandType.StoredProcedure));
            }
        }

        public override async Task<IEnumerable<GetProductDto>> GetListAsync()
        {
            using (IDbConnection db = CreateConnection())
            {
                var result = await db.QueryAsync<ProductEntity>($"sp_Product_GetList", commandType: CommandType.StoredProcedure);
                return _mapper.Map<IEnumerable<GetProductDto>>(result);
            }
        }

        public override async Task<bool> UpdateAsync(UpdateProductParams param)
        {
            using (IDbConnection db = CreateConnection())
            {
                var list = param.GetType().GetProperties()
                    .Where(p => p.GetValue(param) != null)
                    .ToList();
                Dictionary<string, object> spParams = new Dictionary<string, object>();

                list.ForEach(x =>
                {
                    spParams[x.Name] = x.GetValue(param);
                });

                int result = await db.ExecuteAsync($"sp_Product_Update", spParams, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }
    }
}
