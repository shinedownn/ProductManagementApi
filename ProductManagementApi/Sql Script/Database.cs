using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

namespace ProductManagementApi.Sql_Script
{
    public class Database
    {
        public readonly string _connectionString;
        public Database(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        } 

        public async Task<bool> Create() {             
            var fullPath = Environment.CurrentDirectory + "\\Sql Script\\Init.sql"; 
            string script = File.ReadAllText(fullPath);
            var batches = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                db.Open(); 

                foreach (var batch in batches)
                {
                    if (string.IsNullOrWhiteSpace(batch)) continue;

                    using (var command = new SqlCommand(batch, db))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                db.Close();
                return await Task.FromResult(true); ;
            }
             
        } 
    }
}
