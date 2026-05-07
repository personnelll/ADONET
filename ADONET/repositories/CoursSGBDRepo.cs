using ADONET.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADONET.repositories
{
    public class CoursSGBDRepo
    {
        private readonly string _connectionString = "public static string cheminDB = @\"Server = PC_FAMILIAL\\SQL2025; Database = CoursSGBD; User Id = sa; Password = Ephec2025;TrustServerCertificate=True\";";

        private readonly ILogger<CoursSGBDRepo> _logger;
        public CoursSGBDRepo()
        {
        }

        public List<Students> GetAll()
        {
            List<Students> list = new List<Students>();
            _logger.LogDebug("Connecting to database with connection string: {ConnectionString}", _connectionString);
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

            using ( SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
            }

            return list;
        }
    }
}
