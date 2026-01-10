using AutoMapper;
using Dapper;
using ProductManagementApi.DataAccess.Abstract;
using ProductManagementApi.Entities.Concrete;
using ProductManagementApi.Entities.Dtos.UserDto;
using ProductManagementApi.Utility;
using System.Data;

namespace ProductManagementApi.DataAccess.Concrete
{
    public class UserRepository : IUserRepository
    { 
        private readonly IMapper _mapper;

        public UserRepository(IConfiguration configuration, IMapper mapper) : base(configuration)
        { 
            _mapper = mapper;
        }

        public override async Task<bool> CreateAsync(CreateUserDto param)
        {
            using (IDbConnection db = CreateConnection())
            {  
                var entity = _mapper.Map<UserEntity>(param);
                entity.PasswordHash= PasswordUtility.Encrypt(param.Password);
                var result = await db.ExecuteScalarAsync<int>($"sp_User_Create",entity,commandType: CommandType.StoredProcedure) > 0;
                return result;
            }
        }

        public override async Task<bool> DeleteAsync(DeleteUserDto param)
        {
            using (IDbConnection db = CreateConnection())
            {
                var list = param.GetType().GetProperties()
                    .Where(p => p.GetValue(param) != null)
                    .ToList();
                Dictionary<string, object> spParams = new Dictionary<string, object>();

                list.ForEach(x =>
                {
                    spParams[x.Name] = x.GetValue(param);
                });
                int result = await db.ExecuteAsync($"sp_User_Delete", spParams, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public override async Task<GetUserDto> GetAsync(GetUserDto param)
        {
            using (IDbConnection db = CreateConnection())
            {
                var list = param.GetType().GetProperties()
                    .Where(p => p.GetValue(param) != null)
                    .ToList();
                Dictionary<string, object> spParams = new Dictionary<string, object>();

                list.ForEach(x =>
                {
                    spParams[x.Name] = x.GetValue(param);
                });
                var dbEntity = await db.QueryFirstOrDefaultAsync<UserEntity>($"sp_User_Get", spParams, commandType: CommandType.StoredProcedure);

                return _mapper.Map<GetUserDto>(dbEntity);
            }
        }

        public override async Task<IEnumerable<GetUserDto>> GetListAsync()
        {
            using (IDbConnection db = CreateConnection())
            {
                return _mapper.Map<IEnumerable<GetUserDto>>(await db.QueryAsync<IEnumerable<UserEntity>>($"sp_User_GetList",commandType:CommandType.StoredProcedure));
            }
        }

        public override async Task<bool> UpdateAsync(UpdateUserDto param)
        {
            using (IDbConnection db = CreateConnection())
            {
                var list = param.GetType().GetProperties()
                    .Where(p => p.GetValue(param) != null)
                    .ToList();
                Dictionary<string, object> spParams = new Dictionary<string, object>();

                list.ForEach(x =>
                {
                    spParams[x.Name] = x.GetValue(param);
                });

                int result = await db.ExecuteAsync($"sp_User_Update", spParams, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }
    }
}
