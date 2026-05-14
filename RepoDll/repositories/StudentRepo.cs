using Interfaces;
using Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
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

            string sql = GetFileFromAssemblyAsync("repositories.SQL.Etudiant_GetAll.sql");
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using(SqlCommand command = new SqlCommand(sql, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var student = new Students
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("ETU_id")),
                            matricule = reader.GetString(reader.GetOrdinal("ETU_NOM")),
                            firstName = reader.IsDBNull(reader.GetOrdinal("ETU_PRENOM")) ? null : reader.GetString(reader.GetOrdinal("ETU_PRENOM")),
                            lastName = reader.GetString(reader.GetOrdinal("ETU_MATRICULE"))
                        };
                        list.Add(student);
                    }
                }
            }

                return list;
        }

        public List<Students> GetByLastName(string lastName)
        {
            List<Students> list = new List<Students>();
            string sql = GetFileFromAssemblyAsync("repositories.SQL.Etudiant_GetByLastName.sql");
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ETU_NOM", lastName);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var student = new Students
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("ETU_id")),
                                matricule = reader.GetString(reader.GetOrdinal("ETU_NOM")),
                                firstName = reader.IsDBNull(reader.GetOrdinal("ETU_PRENOM")) ? null : reader.GetString(reader.GetOrdinal("ETU_PRENOM")),
                                lastName = reader.GetString(reader.GetOrdinal("ETU_MATRICULE"))
                            };
                            list.Add(student);
                        }
                    }
                }
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
