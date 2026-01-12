using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementApi.Entities.Dtos.ProductDto;
using ProductManagementApi.Entities.EndpointParams.Product;
using ProductManagementApi.Response;
using System.ComponentModel.DataAnnotations;

namespace ProductManagementApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// Tüm ürünleri listeler
        /// </summary>
        /// <returns>Ürün listesini döner</returns>
        [HttpGet] // GET /api/products
        public async Task<ResponseModel<IEnumerable<GetProductDto>>> GetAll()
        {             
              return await new Business.Product().GetAllAsync();               
        }
        /// <summary>
        /// Id numaralı ürünü gösterir
        /// </summary>
        /// <param name="id">ürün id numarası</param>
        /// <returns>Ürünü döner</returns>
        [HttpGet("{id}")] // GET /api/products/{id}
        public async Task<ResponseModel<GetProductDto>> GetById([Range(1, int.MaxValue, ErrorMessage = "ID 0'dan büyük olmalıdır.")] int id)
        {
            return await new Business.Product().GetAsync(id);
        }
        /// <summary>
        /// Ürün ekler
        /// </summary>
        /// <param name="param">Ürün parametreleri</param>
        /// <returns>True veya False döner</returns>
        [HttpPost] // POST /api/products
        public async Task<ResponseModel<bool>> Create(CreateProductParams param)
        {
            return await new Business.Product().CreateAsync(param); 
        }
        /// <summary>
        /// Id numaralı Ürünü günceller
        /// </summary>
        /// <param name="id">Ürünün id numarası</param>
        /// <param name="param">Ürünün güncellenecek değerleri</param>
        /// <returns>True veya False döner</returns>
        [HttpPut("{id}")] // PUT /api/products/{id}
        public async Task<ResponseModel<bool>> Update([Range(0, int.MaxValue, ErrorMessage = "ID 0'dan büyük olmalıdır.")] int id,[FromBody] UpdateProductParams param)
        {
            return await new Business.Product().UpdateAsync(id,param);             
        }
        /// <summary>
        /// Id numaralı Ürünü siler
        /// </summary>
        /// <param name="id">Ürünün Id numarası</param>
        /// <returns>True veya False döner</returns>
        [HttpDelete("{id}")] // DELETE /api/products/{id}
        public async Task<ResponseModel<bool>> Delete([Range(1, int.MaxValue, ErrorMessage = "ID 0'dan büyük olmalıdır.")] int id)
        {
            return await new Business.Product().DeleteAsync(id);            
        }
    }
}
