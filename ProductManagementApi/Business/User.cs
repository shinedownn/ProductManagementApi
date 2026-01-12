using ProductManagementApi.DataAccess.Abstract;
using ProductManagementApi.Entities.Concrete;
using ProductManagementApi.Entities.EndpointParams.User;
using ProductManagementApi.Helpers;
using ProductManagementApi.Response;
using ProductManagementApi.Utility;

namespace ProductManagementApi.Business
{
    public class User
    {
        private readonly IUserRepository _userRepository;
        public User()
        {
            _userRepository = ServiceHelper.Services.GetService<IUserRepository>();
        }

        public async Task<ResponseModel<string>> LoginAsync(LoginParams param)
        {
            var responseModel = new ResponseModel<string>();
            try
            {
                var passwordHash = PasswordUtility.Encrypt(param.Password).ToString();
                var user = await _userRepository.GetAsync(new UserEntity() { Username=param.Username, PasswordHash=passwordHash});
                if(user != null)
                {
                    responseModel.Data = TokenUtility.GenerateJSONWebToken();
                    responseModel.Status = true;
                    responseModel.Message = "Giriş başarılı";
                    return responseModel;
                }
                else
                {
                    responseModel.Status = false;
                    responseModel.Message = "Kullanıcı bulunamadı";
                }
            }
            catch (Exception ex)
            {
                responseModel.Message = "Hata!";
                responseModel.Errors.Add(ex.Message);
                responseModel.Status = false;
                responseModel.Data = null;
            }

            return responseModel;
        }

    }
}
