using ProductManagementApi.DataAccess.Abstract;
using ProductManagementApi.Entities.Concrete;
using ProductManagementApi.Helpers;
using ProductManagementApi.Response;
using ProductManagementApi.Services.FakestoreApi.Abstract;
using ProductManagementApi.Services.FakestoreApi.Models;

namespace ProductManagementApi.Business
{
    public class ExternalProduct
    {         
        private readonly IExternalProductService _externalProductService;
        private readonly IProductRepository _productRepository = ServiceHelper.Services.GetService<IProductRepository>();
        public ExternalProduct()
        {             
            _externalProductService = ServiceHelper.Services.GetService<IExternalProductService>();
        }

        public async Task<ResponseModel<IEnumerable<ExternalProductModel>>> GetProducts()
        {
            var responseModel = new ResponseModel<IEnumerable<ExternalProductModel>>();
            try
            {
                var products = await _externalProductService.GetProducts();
                responseModel.Data = products;
                responseModel.Status = true;
                responseModel.Message = "Başarılı";
            }
            catch (Exception ex)
            {
                responseModel.Data = null;
                responseModel.Status = false;
                responseModel.Message = "Başarısız";
                responseModel.Errors.Add(ex.Message);
            }
            return responseModel;
        }

        public async Task<ResponseModel<IEnumerable<ExternalProductModel>>> GetProductByRandomPrice()
        {
            var responseModel = new ResponseModel<IEnumerable<ExternalProductModel>>();
            try
            {
                var products = await _externalProductService.GetProductByRandomPrice();
                responseModel.Data = products;
                responseModel.Status = true;
                responseModel.Message = "Başarılı";
            }
            catch (Exception ex)
            {
                responseModel.Data = null;
                responseModel.Status = false;
                responseModel.Message = "Başarısız";
                responseModel.Errors.Add(ex.Message);
            }
            return responseModel;
        }

        public async Task<ResponseModel<PriceSyncReport>> GetDifferentProducts()
        {
            var localList = await _productRepository.GetListAsync();
            var externalList = await _externalProductService.GetProducts();
            var report = new PriceSyncReport();
            var externalMapped = externalList
                    .Where(e => !string.IsNullOrWhiteSpace(e.Title))
                    .Select(e => new
                    {
                        CleanName = e.Title.Trim().ToLower(),
                        e.Price
                    });

            var localData = localList
                .Select(p => new { p.Id, p.Name, p.Price });

            var toUpdate = localData.Join(externalMapped,
                    l => l.Name.Trim().ToLower(),
                    e => e.CleanName,
                    (l, e) => new { Local = l, External = e })
                    .Where(x => x.Local.Price != x.External.Price)
                    .Select(x =>
                    {
                        var detail = new PriceChangeDetail(x.Local.Name, x.Local.Price, x.External.Price);

                        if (x.External.Price > x.Local.Price) report.Increased.Add(detail);
                        else report.Decreased.Add(detail);

                        return new ProductEntity
                        {
                            Id = x.Local.Id,
                            Price = x.External.Price
                        };
                    }).ToList();
            return new ResponseModel<PriceSyncReport>
            {
                Status = true,
                Message = $"{report.Total} adet fiyatı değişmiş ürün bulundu",
                Data = report
            };
        }

        public async Task<ResponseModel<bool>> BulkInsertToDatabaseWithRandomPrice()
        {
            var responseModel = new ResponseModel<bool>();
            try
            {
                var randomPriceProducts = await _externalProductService.GetProductByRandomPrice();
                var result = await _productRepository.BulkInsertAsync(
                    randomPriceProducts.Select(x => new ProductEntity
                    {
                        Name = x.Title,
                        Category = x.Category,
                        Price = x.Price,
                        CreatedAt = DateTime.Now,
                        IsActive = true
                    }));
                responseModel.Data = result;
                responseModel.Status = true;
                responseModel.Message = "Başarılı";
            }
            catch (Exception ex)
            {
                responseModel.Data = false;
                responseModel.Status = false;
                responseModel.Message = "Başarısız";
                responseModel.Errors.Add(ex.Message);
            }
            return responseModel;
        }

    }
    public class PriceSyncReport
    {
        public List<PriceChangeDetail> Increased { get; set; } = new();
        public List<PriceChangeDetail> Decreased { get; set; } = new();
        public int Total => Increased.Count + Decreased.Count;
    }

    public record PriceChangeDetail(string Name, decimal LocalPrice, decimal ExternalPrice);
}
