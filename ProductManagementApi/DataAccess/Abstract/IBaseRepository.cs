using AutoMapper;
using Dapper;
using ProductManagementApi.Entities;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Options;

namespace ProductManagementApi.DataAccess.Abstract
{
    public abstract class IBaseRepository<T>
        where T : class, IEntity, new()
    {
        private readonly AppSettingsModel _appSettings;

        protected IBaseRepository(IOptions<AppSettingsModel> appSettings)
        {             
            _appSettings = appSettings.Value;
        }
        public abstract Task<T> GetAsync(int id);
        public abstract Task<T> GetAsync(T where);
        public abstract Task<bool> UpdateAsync(T entity);
        public abstract Task<bool> DeleteAsync(int id);
        public abstract Task<bool> CreateAsync(T entity);
        public abstract Task<IEnumerable<T>> GetListAsync();
         
        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_appSettings.ConnectionStrings.DefaultConnection);
        }
    }
}
