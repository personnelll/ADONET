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
            string sql = GetFileFromAssemblyAsync("Etudiant_add.sql");
            Dictionary<string, object> dbArgs = new Dictionary<string, object>();
            dbArgs.Add("@Nom", student.lastName);
            dbArgs.Add("@Prenom", student.firstName);
            dbArgs.Add("@Matricule", student.matricule);
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                int rowsAffected = connection.Execute(sql, dbArgs);
                _logger.LogInformation("{RowsAffected} row(s) inserted.", rowsAffected);
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Students student)
        {
            throw new NotImplementedException();
        }

    }
}
