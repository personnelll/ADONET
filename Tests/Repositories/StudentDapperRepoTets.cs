using Castle.Core.Logging;
using DotNet.Testcontainers.Builders;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging.Abstractions;
using Repositories;
using Shared;
using Testcontainers.MsSql;

namespace Tests.RepositoriesTests
{
    public class StudentDapperRepoTets
    {
        private MsSqlContainer _container;

        [Fact]
        public async Task GetAllTest()
        {
            var container = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                .WithPassword("Your_strong(!)Password")
                .Build();

            await container.StartAsync();

            DBSetup dbSetup = new DBSetup(container.GetConnectionString());

            await dbSetup.CreatDBAsync();

            await dbSetup.CreatTablesAsync();

            await dbSetup.InitStudentsDataAsync();

            try
            {
                string connectionString = container.GetConnectionString();
                connectionString.Replace("master;", "CoursSGBD");

                var logger = NullLogger<StudentDapperRepo>.Instance;
                var repo = new StudentDapperRepo(logger, connectionString);

                var students = repo.GetAll();

                Assert.NotNull(students);
                Assert.NotEmpty(students);
                Assert.Equal(3, students.Count);
            }
            finally
            {
                await container.DisposeAsync();
            }
        }

        [Theory]
        [InlineData("Platiau",1)]
        [InlineData("Dupont", 0)]
        [InlineData("Pironet", 1)]
        public async Task FindStudentsByLastName(string search, int result)
        {
            var container = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                .WithPassword("Your_strong(!)Password")
                .Build();

            await container.StartAsync();

            DBSetup dbSetup = new DBSetup(container.GetConnectionString());

            await dbSetup.CreatDBAsync();

            await dbSetup.CreatTablesAsync();

            await dbSetup.InitStudentsDataAsync();

            try
            {
                string connectionString = container.GetConnectionString();
                connectionString.Replace("master;", "CoursSGBD");

                var logger = NullLogger<StudentDapperRepo>.Instance;
                var repo = new StudentDapperRepo(logger, connectionString);

                //act
                var students = repo.GetByLastName(search);
                //assert
                Assert.NotNull(students);
                Assert.NotEmpty(students);
                Assert.Equal(result, students.Count);
            }
            finally
            {
                await container.DisposeAsync();
            }
        }
    }
}
