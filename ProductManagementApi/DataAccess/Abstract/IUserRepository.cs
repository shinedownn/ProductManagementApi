using AutoMapper;
using ProductManagementApi.Entities.Concrete;
using ProductManagementApi.Entities.Dtos.UserDto;
using ProductManagementApi.Entities.EndpointParams;
using ProductManagementApi.Entities.EndpointParams.User;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ProductManagementApi.DataAccess.Abstract
{
    public abstract class IUserRepository
    {
        private readonly string _connectionString;
        private IConfiguration configuration;

        protected IUserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public abstract Task<GetUserDto> GetAsync(GetUserDto param);
        public abstract Task<bool> UpdateAsync(UpdateUserDto param);
        public abstract Task<bool> DeleteAsync(DeleteUserDto param);
        public abstract Task<bool> CreateAsync(CreateUserDto param);
        public abstract Task<IEnumerable<GetUserDto>> GetListAsync();
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
