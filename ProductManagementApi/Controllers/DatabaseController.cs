using ProductManagementApi.Response;
using ProductManagementApi.Sql_Script;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        Database _database; 
        public DatabaseController(IConfiguration configuration)
        {
            _database = new Database(configuration);
        }
        /// <summary>
        /// Veritabanı scriptini çalıştırır. Tabloları ve prosedürleri ekler. Boş bir veritabanı açınız ve appsettings.Development.json dosyasına yazınız
        /// </summary>
        /// <returns>"Database oluşturuldu" veya "Database oluşturulamadı" cevabı döner</returns>
        [HttpPost]
        public async Task<ResponseModel<bool>> Create()
        {
            ResponseModel<bool> responseModel = new();
            try
            {
                var result = await _database.Create();

                responseModel.Status = result;
                responseModel.Data = result;
                responseModel.Message = result ? "Database oluşturuldu" : "Database oluşturulamadı";
            }
            catch (Exception ex)
            {
                responseModel.Message = "Database oluşturulamadı";
                responseModel.Status = false;
                responseModel.Message = ex.Message;
                responseModel.Errors.Add(ex.Message);

            }
            return responseModel;
        }
    }
}
