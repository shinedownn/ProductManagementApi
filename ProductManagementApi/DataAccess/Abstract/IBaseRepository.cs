using AutoMapper;
using Dapper;
using ProductManagementApi.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ProductManagementApi.DataAccess.Abstract
{
    public abstract class IBaseRepository<T>
        where T : class, IEntity, new()
    {
        public readonly string _connectionString;         
        protected IBaseRepository(IConfiguration configuration)
        { 
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public abstract Task<T> GetAsync(T where);
        public abstract Task<bool> UpdateAsync(T entity);
        public abstract Task<bool> DeleteAsync(int id);
        public abstract Task<bool> CreateAsync(T entity);
        public abstract Task<IEnumerable<T>> GetListAsync(); 
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
