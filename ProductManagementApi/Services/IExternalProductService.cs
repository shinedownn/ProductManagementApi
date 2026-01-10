namespace ProductManagementApi.Services
{
    public interface IExternalProductService
    {
        Task<IEnumerable<ExternalProduct>> GetProducts();
        Task<IEnumerable<ExternalProduct>> GetProductByRandomPrice();
    }
}
