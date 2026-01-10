using ProductManagementApi.Entities.Dtos.ProductDto;
using ProductManagementApi.Entities.EndpointParams.Product;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ProductManagementApi.DataAccess.Abstract
{
    public abstract class IProductRepository
    {
        private readonly string _connectionString;

        public IProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public abstract Task<GetProductDto> GetAsync(GetProductParams param);
        public abstract Task<bool> UpdateAsync(UpdateProductParams param);
        public abstract Task<bool> DeleteAsync(DeleteProductParams param);
        public abstract Task<bool> CreateAsync(CreateProductParams param);
        public abstract Task<IEnumerable<GetProductDto>> GetListAsync();
        public abstract Task<bool> BulkInsert(IEnumerable<CreateProductParams> paramList);
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
