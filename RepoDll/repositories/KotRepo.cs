using Dapper;
using Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Models;
using DTO;
using System.Data;
namespace Repositories
{
    public class KotRepo : BaseRepo, IKotRepo
    {
        private readonly string _connectionString = @"Server = PC_FAMILIAL\SQL2025; Database = CoursSGBD; User Id = sa; Password = Ephec2025;TrustServerCertificate=True;";
        private readonly ILogger<KotRepo> _logger;
        public KotRepo(ILogger<KotRepo> logger)
        {
            _logger = logger;
        }

        public KotRepo(ILogger<KotRepo> logger, string connectionString)
        {
            _logger = logger;
            _connectionString = connectionString;
        }

        public List<KotStudentDTO> GetAll()
        {
            List<KotStudentDTO> kots = new List<KotStudentDTO>();
            string sql = GetFileFromAssemblyAsync("Kots_SelectAll.sql");
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                kots = connection.Query<KotStudentDTO>(sql).ToList();
            }
            return kots;
        }

        public List<Kots> GetByName(string name)
        {
            List<Kots> list = new List<Kots>();
            string sql = GetFileFromAssemblyAsync("Kot_FindByNameDapper.sql");

            Dictionary<string, object> dbArgs = new Dictionary<string, object>();
            dbArgs.Add("@Name", name);

            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                list = connection.Query<Kots>(sql, dbArgs).ToList();
            }
            return list;
        }

        public void Add(Kots kot)
        {
            string sql = GetFileFromAssemblyAsync("Kot_add.sql");
            Dictionary<string, object> dbArgs = new Dictionary<string, object>();
            dbArgs.Add("@Nom", kot.nom);
            dbArgs.Add("@Resident", kot.Id);
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                int rowsAffected = connection.Execute(sql, dbArgs);
                _logger.LogInformation("{RowsAffected} row(s) inserted.", rowsAffected);
            }
        }

        public void Delete(int id)
        {
            string sql = GetFileFromAssemblyAsync("Kot_delete.sql");

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    _logger.LogInformation("{RowsAffected} row(s) deleted.", rowsAffected);
                }
            }
        }

        public void Update(Kots kot)
        {
            throw new NotImplementedException();
        }

    }
}
