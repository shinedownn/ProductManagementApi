using AutoMapper;
using ProductManagementApi.DataAccess.Abstract;
using ProductManagementApi.Entities.Concrete;
using ProductManagementApi.Entities.Dtos.ProductDto;
using ProductManagementApi.Entities.EndpointParams.Product;
using ProductManagementApi.Helpers;
using ProductManagementApi.Response;

namespace ProductManagementApi.Business
{
    public class Product
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public Product()
        {
            _productRepository = ServiceHelper.Services.GetService<IProductRepository>();
            _mapper = ServiceHelper.Services.GetService<IMapper>();
        }

        public async Task<ResponseModel<GetProductDto>> GetAsync(int id)
        {
            var responseModel = new ResponseModel<GetProductDto>();
            try
            {
                var product = await _productRepository.GetAsync(id);
                if (product != null)
                {
                    responseModel.Data = _mapper.Map<GetProductDto>(product);
                    responseModel.Status = true;
                    responseModel.Message = "Ürün bulundu";
                }
                else
                {
                    responseModel.Status = false;
                    responseModel.Message = "Ürün bulunamadı";
                }
            }
            catch (Exception ex)
            {
                responseModel.Message = "Hata!";
                responseModel.Errors.Add(ex.Message);
                responseModel.Status = false;
            }
            return responseModel;
        }

        public async Task<ResponseModel<IEnumerable<GetProductDto>>> GetAllAsync()
        {
            var responseModel = new ResponseModel<IEnumerable<GetProductDto>>();
            try
            {
                var products = await _productRepository.GetListAsync();
                responseModel.Data = _mapper.Map<IEnumerable<GetProductDto>>(products);
                responseModel.Status = true;
                responseModel.Message = "Ürünler listelendi";
            }
            catch (Exception ex)
            {
                responseModel.Message = "Hata!";
                responseModel.Errors.Add(ex.Message);
                responseModel.Status = false;
            }
            return responseModel;
        }

        public async Task<ResponseModel<bool>> CreateAsync(CreateProductParams newProduct)
        {
            var responseModel = new ResponseModel<bool>();
            try
            {
                var productEntity = _mapper.Map<ProductEntity>(newProduct);
                var result = await _productRepository.CreateAsync(productEntity);
                responseModel.Data = result;
                responseModel.Status = result;
                responseModel.Message = result ? "Ürün eklendi" : "Ürün eklenme sırasında hata oldu";
            }
            catch (Exception ex)
            {
                responseModel.Message = "Hata!";
                responseModel.Errors.Add(ex.Message);
                responseModel.Status = false;
            }
            return responseModel;
        }
        public async Task<ResponseModel<bool>> DeleteAsync(int id)
        {
            var responseModel = new ResponseModel<bool>();
            try
            {
                var result = await _productRepository.DeleteAsync(id);
                responseModel.Data = result;
                responseModel.Status = result;
                responseModel.Message = result ? "Ürün silindi" : "Ürün bulunamadı";
            }
            catch (Exception ex)
            {
                responseModel.Message = "Hata!";
                responseModel.Errors.Add(ex.Message);
                responseModel.Status = false;
            }
            return responseModel;
        }

        public async Task<ResponseModel<bool>> UpdateAsync(int id,UpdateProductParams updatedProduct)
        {
            var responseModel = new ResponseModel<bool>();
            try
            {
                var productEntity = _mapper.Map<ProductEntity>(updatedProduct);
                productEntity.Id = id;
                var result = await _productRepository.UpdateAsync(productEntity);
                responseModel.Data = result;
                responseModel.Status = result;
                responseModel.Message = result ? "Ürün güncellendi" : "Ürün bulunamadı";
            }
            catch (Exception ex)
            {
                responseModel.Message = "Hata";
                responseModel.Errors.Add(ex.Message);
                responseModel.Status = false;
            }
            return responseModel;
        }

         
    }
}
