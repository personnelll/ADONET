using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class DBSetup : BaseRepo
    {
        private readonly string _connectionString;
        
        public DBSetup(string connectionstring)
        {
            _connectionString = connectionstring;
        }

        public async Task CreatDBAsync()
        {
            await RunScript("CreateDB.sql");
        }

        public async Task CreatTablesAsync()
        {
            await RunScript("CreateTable.sql");
            
        }

        public async Task InitStudentsDataAsync()
        {
            await RunScript("initStudentsData.sql");
        }

        private async Task RunScript(string filename)
        {
            string sql = GetFileFromAssemblyAsync(filename);

            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                int rowsAffected = await connection.ExecuteAsync(sql);
            }
        }
    }
}
