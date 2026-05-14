using Dapper;
using Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class StudentDapperRepo : BaseRepo, IStudentRepo
    {
        private readonly string _connectionString = @"Server = PC_FAMILIAL\SQL2025; Database = CoursSGBD; User Id = sa; Password = Ephec2025;TrustServerCertificate=True;";
        private readonly ILogger<StudentADONETRepo> _logger;
        public StudentDapperRepo(ILogger<StudentADONETRepo> logger)
        {
            _logger = logger;
        }

        public List<Students> GetAll()
        {
            List<Students> students = new List<Students>();
            string sql = GetFileFromAssemblyAsync("Etudiant_SelectAllDapper.sql");
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                students = connection.Query<Students>(sql).ToList();
            }
            return students;
        }

        public List<Students> GetByLastName(string lastName)
        {
            List<Students> list = new List<Students>();
            string sql = GetFileFromAssemblyAsync("Etudiant_FindByLastNameDapper.sql");

            Dictionary<string, object> dbArgs = new Dictionary<string, object>();
            dbArgs.Add("@lastName", lastName);

            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                list = connection.Query<Students>(sql, dbArgs).ToList();
            }
            return list;
        }

        public void Add(Students student)
        {
            //using (SqlConnection connection = new SqlConnection(_connectionString))
            //{
            //    connection.Open();
            //    string sql = GetFileFromAssemblyAsync("repositories.SQL.Etudiant_add.sql");

            //    using (SqlCommand command = new SqlCommand(sql, connection))
            //    {
            //        command.Parameters.AddWithValue("@Nom", student.lastName);
            //        command.Parameters.AddWithValue("@Prenom", student.firstName);
            //        command.Parameters.AddWithValue("@Matricule", student.matricule);
            //        int rowsAffected = command.ExecuteNonQuery();
            //        _logger.LogInformation("Inserted {RowsAffected} row(s) into the database.", rowsAffected);
            //    }
            //}
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            //using (SqlConnection connection = new SqlConnection(_connectionString))
            //{
            //    string sql = GetFileFromAssemblyAsync("repositories.SQL.Etudiant_delete.sql");
            //    connection.Open();
            //    using (SqlCommand command = new SqlCommand(sql, connection))
            //    {
            //        command.Parameters.AddWithValue("@Id", id);
            //        int rowsAffected = command.ExecuteNonQuery();
            //        _logger.LogInformation("Deleted {RowsAffected} row(s) from the database.", rowsAffected);
            //    }
            //}
            throw new NotImplementedException();
        }

        public void Update(Students student)
        {
            //using (SqlConnection connection = new SqlConnection(_connectionString))
            //{
            //    connection.Open();
            //    string sql = GetFileFromAssemblyAsync("repositories.SQL.Etudiant_update.sql");

            //    using (SqlCommand command = new SqlCommand(sql, connection))
            //    {
            //        command.Parameters.AddWithValue("@Id", student.Id);
            //        command.Parameters.AddWithValue("@Nom", student.lastName);
            //        command.Parameters.AddWithValue("@Prenom", student.firstName);
            //        command.Parameters.AddWithValue("@Matricule", student.matricule);
            //        int rowsAffected = command.ExecuteNonQuery();
            //        _logger.LogInformation("Update {RowsAffected} row(s) into the database.", rowsAffected);
            //    }
            //}
            throw new NotImplementedException();
        }

    }
}
