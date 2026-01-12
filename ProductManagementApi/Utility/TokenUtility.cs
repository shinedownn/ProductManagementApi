using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProductManagementApi.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ProductManagementApi.Utility
{
    public class TokenUtility
    {
        private static readonly AppSettingsModel _appsettings = ServiceHelper.Services.GetRequiredService<IOptions<AppSettingsModel>>().Value; 
        
        public static string GenerateJSONWebToken()
        { 
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appsettings.Jwt.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_appsettings.Jwt.Issuer,
              _appsettings.Jwt.Issuer,
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
