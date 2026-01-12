using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementApi.Entities.EndpointParams.User;
using ProductManagementApi.Response;

namespace ProductManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {         
        public TokenController()
        {
            
        }
        /// <summary>
        /// Veritabanı scriptini çalıştırdıktan sonra token alabilmek için Kullanıcı adı = demo , Password = demo bilgilerini kullanabilirsiniz.
        /// </summary>
        /// <param name="param">Login parametreleri</param>
        /// <returns>Token</returns>
                
        [AllowAnonymous]
        [HttpPost]
        public async Task<ResponseModel<string>> Get(LoginParams param)
        {
            var responseModel = await new Business.User().LoginAsync(param);
            return responseModel;
            //ResponseModel<string> responseModel = new();
            //var passwordHash = PasswordUtility.Encrypt(param.Password).ToString();

            //try
            //{
            //    var user = await _userRepository.GetAsync(new Entities.Dtos.UserDto.GetUserDto()
            //    {
            //        Username = param.Username,
            //        PasswordHash = passwordHash
            //    });

            //    if (user != null)
            //    {
            //        IActionResult response = Unauthorized();

            //        var tokenString = _tokenutility.GenerateJSONWebToken();

            //        responseModel.Data = tokenString;
            //        responseModel.Status = true;
            //        return responseModel;
            //    }

            //    responseModel.Message = "Kullanıcı bulunamadı";
            //    responseModel.Errors.Add("Kullanıcı bulunamadı");  
            //}
            //catch (Exception ex)
            //{
            //    responseModel.Status = false;
            //    responseModel.Message = ex.Message;
            //    responseModel.Errors.Add(ex.Message);
            //}

            //return responseModel;
        }         
    }
}
