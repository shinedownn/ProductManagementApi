using ProductManagementApi.DataAccess.Abstract;
using ProductManagementApi.Entities.Dtos.ProductDto;
using ProductManagementApi.Entities.EndpointParams.Product;
using ProductManagementApi.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations; 

namespace ProductManagementApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;         
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        /// <summary>
        /// Tüm ürünleri listeler
        /// </summary>
        /// <returns>Ürün listesini döner</returns>
        [HttpGet] // GET /api/products
        public async Task<ResponseModel<IEnumerable<GetProductDto>>> GetAll()
        {
            ResponseModel<IEnumerable<GetProductDto>> responseModel = new();
            try
            {
                var result = await _productRepository.GetListAsync();

                responseModel.Status = result != null;
                responseModel.Data = result;
                responseModel.Message = result != null ? "Başarılı" : "Bulunamadı";
            }
            catch (Exception ex)
            {
                responseModel.Status = false;
                responseModel.Message = ex.Message;
                responseModel.Errors.Add(ex.Message);

            }

            return responseModel;
        }
        /// <summary>
        /// Id numaralı ürünü gösterir
        /// </summary>
        /// <param name="id">ürün id numarası</param>
        /// <returns>Ürünü döner</returns>
        [HttpGet("{id}")] // GET /api/products/{id}
        public async Task<ResponseModel<GetProductDto>> GetById([Range(1, int.MaxValue, ErrorMessage = "ID 0'dan büyük olmalıdır.")] int id)
        {
            ResponseModel<GetProductDto> responseModel = new();
            try
            {
                var result = await _productRepository.GetAsync(new GetProductParams() { Id=id});

                responseModel.Status = result != null;
                responseModel.Data = result;
                responseModel.Message = result != null ? "Başarılı" : "Bulunamadı";
            }
            catch (Exception ex)
            {
                responseModel.Status = false;
                responseModel.Message = ex.Message;
                responseModel.Errors.Add(ex.Message);

            }

            return responseModel;
        }
        /// <summary>
        /// Ürün ekler
        /// </summary>
        /// <param name="param">Ürün parametreleri</param>
        /// <returns>True veya False döner</returns>
        [HttpPost] // POST /api/products
        public async Task<ResponseModel<bool>> Create(CreateProductParams param)
        {
            ResponseModel<bool> responseModel = new();
            try
            {
                var result = await _productRepository.CreateAsync(param);

                responseModel.Status = result;
                responseModel.Data = result;
                responseModel.Message = result ? "Başarılı" : "Başarısız";
            }
            catch (Exception ex)
            {
                responseModel.Status = false;
                responseModel.Message = ex.Message;
                responseModel.Errors.Add(ex.Message);

            }
            return responseModel;
        }
        /// <summary>
        /// Id numaralı Ürünü günceller
        /// </summary>
        /// <param name="id">Ürünün id numarası</param>
        /// <param name="param">Ürünün güncellenecek değerleri</param>
        /// <returns>True veya False döner</returns>
        [HttpPut("{id}")] // PUT /api/products/{id}
        public async Task<ResponseModel<bool>> Update([Range(1, int.MaxValue, ErrorMessage = "ID 0'dan büyük olmalıdır.")] int id, UpdateProductParams param)
        {
            ResponseModel<bool> responseModel = new();
            try
            {
                var result = await _productRepository.UpdateAsync(param);

                responseModel.Status = result;
                responseModel.Data = result;
                responseModel.Message = result ? "Başarılı" : "Başarısız";
            }
            catch (Exception ex)
            {
                responseModel.Status = false;
                responseModel.Message = ex.Message;
                responseModel.Errors.Add(ex.Message);

            }
            return responseModel;
        }
        /// <summary>
        /// Id numaralı Ürünü siler
        /// </summary>
        /// <param name="id">Ürünün Id numarası</param>
        /// <returns>True veya False döner</returns>
        [HttpDelete("{id}")] // DELETE /api/products/{id}
        public async Task<ResponseModel<bool>> Delete([Range(1, int.MaxValue, ErrorMessage = "ID 0'dan büyük olmalıdır.")] int id)
        {
            ResponseModel<bool> responseModel = new();
            try
            {
                var result = await _productRepository.DeleteAsync(new DeleteProductParams() { Id=id});

                responseModel.Status = result;
                responseModel.Data = result;
                responseModel.Message = result ? "Başarılı" : "Başarısız";
            }
            catch (Exception ex)
            {
                responseModel.Status = false;
                responseModel.Message = ex.Message;
                responseModel.Errors.Add(ex.Message);

            }
            return responseModel;
        } 
    }
}
