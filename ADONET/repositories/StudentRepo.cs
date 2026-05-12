using ADONET.Interfaces;
using ADONET.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ADONET.repositories
{
    public class StudentRepo : BaseRepo, IStudentRepo
    {
        private readonly string _connectionString = @"Server = PC_FAMILIAL\SQL2025; Database = CoursSGBD; User Id = sa; Password = Ephec2025;TrustServerCertificate=True;";
        private readonly ILogger<StudentRepo> _logger;
        public StudentRepo(ILogger<StudentRepo> logger)
        {
            _logger = logger;
        }

        public List<Students> GetAll()
        {
            List<Students> list = new List<Students>();
            _logger.LogInformation("Connecting to database with connection string: {ConnectionString}", _connectionString);
            Students student = new Students();
            student.matricule = "2023-001";
            student.firstName = "John";
            student.lastName = "Doe";
            student.email = "mail.gmail.com";

            Students student2 = new Students();
            student2.matricule = "2023-002";
            student2.firstName = "Jane";
            student2.lastName = "Smith";
            student2.email = "mail.yahoo.com";

            list.Add(student);
            list.Add(student2);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

            }

            return list;
        }

        public void Add(Students student)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = GetFileFromAssemblyAsync("repositories.SQL.Etudiant_add.sql");

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Nom", student.lastName);
                    command.Parameters.AddWithValue("@Prenom", student.firstName);
                    command.Parameters.AddWithValue("@Matricule", student.matricule);
                    int rowsAffected = command.ExecuteNonQuery();
                    _logger.LogInformation("Inserted {RowsAffected} row(s) into the database.", rowsAffected);
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = GetFileFromAssemblyAsync("repositories.SQL.Etudiant_delete.sql");
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    _logger.LogInformation("Deleted {RowsAffected} row(s) from the database.", rowsAffected);
                }
            }
        }

        public void Update(Students student)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = GetFileFromAssemblyAsync("repositories.SQL.Etudiant_update.sql");

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", student.Id);
                    command.Parameters.AddWithValue("@Nom", student.lastName);
                    command.Parameters.AddWithValue("@Prenom", student.firstName);
                    command.Parameters.AddWithValue("@Matricule", student.matricule);
                    int rowsAffected = command.ExecuteNonQuery();
                    _logger.LogInformation("Update {RowsAffected} row(s) into the database.", rowsAffected);
                }
            }
        }

    }
}
