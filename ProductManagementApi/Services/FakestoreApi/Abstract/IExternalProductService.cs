using ProductManagementApi.Services.FakestoreApi.Models;

namespace ProductManagementApi.Services.FakestoreApi.Abstract
{
    public interface IExternalProductService
    {
        Task<IEnumerable<ExternalProductModel>> GetProducts();
        Task<IEnumerable<ExternalProductModel>> GetProductByRandomPrice();
    }
}
