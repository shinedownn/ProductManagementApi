using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementApi.Business;
using ProductManagementApi.Response;
using ProductManagementApi.Services.FakestoreApi.Models;

namespace ProductManagementApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalProductController : ControllerBase
    {       
        public ExternalProductController()
        {
            
        }

        /// <summary>
        /// https://fakestoreapi.com/products adresine istek atar
        /// </summary>
        /// <returns>adresteki ürünleri listeler</returns>
        [HttpGet]
        public async Task<ResponseModel<IEnumerable<ExternalProductModel>>> Get()
        {
            return await new Business.ExternalProduct().GetProducts();             
        }

        /// <summary>
        /// https://fakestoreapi.com/products 'dan çektiği ürün listesini iç kaynaktan çektiği ürünlerle karşılaştırır
        /// </summary>
        /// <returns>Fiyatı artanları ve azalanları ayrı ayrı döndürür</returns>
        [HttpGet("GetDifferentProducts")]
        public async Task<ResponseModel<PriceSyncReport>> GetDifferentProducts()
        { 
             return await new Business.ExternalProduct().GetDifferentProducts();
        }

        
        /// <summary>
        /// https://fakestoreapi.com/products 'dan çektiği ürün listesini veritabanına toplu ekler
        /// </summary>
        /// <returns>true veya false döner</returns>
        [HttpPost("BulkInsertToDatabaseWithRandomPrice")]
        public async Task<ResponseModel<bool>> BulkInsertToDatabaseWithRandomPrice()
        {
            return await new Business.ExternalProduct().BulkInsertToDatabaseWithRandomPrice();
        }
    }
    

}
