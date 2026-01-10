using ProductManagementApi.DataAccess.Abstract;
using ProductManagementApi.Entities.EndpointParams.User;
using ProductManagementApi.Response;
using ProductManagementApi.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;
        private readonly TokenUtility _tokenutility;

        public TokenController(IConfiguration config, IUserRepository userRepository)
        {
            _config = config;
            _userRepository = userRepository;
            _tokenutility = new TokenUtility(_config);
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
            ResponseModel<string> responseModel = new();
            var passwordHash = PasswordUtility.Encrypt(param.Password).ToString();

            try
            {
                var user = await _userRepository.GetAsync(new Entities.Dtos.UserDto.GetUserDto()
                {
                    Username = param.Username,
                    PasswordHash = passwordHash
                });

                if (user != null)
                {
                    IActionResult response = Unauthorized();

                    var tokenString = _tokenutility.GenerateJSONWebToken();

                    responseModel.Data = tokenString;
                    responseModel.Status = true;
                    return responseModel;
                }
                
                responseModel.Message = "Kullanıcı bulunamadı";
                responseModel.Errors.Add("Kullanıcı bulunamadı");  
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
