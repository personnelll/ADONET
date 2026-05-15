using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Repositories;
using Shared;
using Testcontainers.MsSql;

namespace Tests.RepositoriesTests
{
    public class StudentDapperRepoTets : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;
        private string _connectionString;
        DBSetup _dbSetup => _fixture.DbSetup;

        public StudentDapperRepoTets(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _connectionString = fixture.ConnectionString;

        }

        [Fact]
        public async Task GetAllTest()
        {
            await _dbSetup.InitStudentsDataAsync();           

            var logger = NullLogger<StudentDapperRepo>.Instance;
            var repo = new StudentDapperRepo(logger, _connectionString);

            var students = repo.GetAll();

            Assert.NotNull(students);
            Assert.NotEmpty(students);
            Assert.Equal(3, students.Count);
            
        }

        [Theory]
        [InlineData("Platiau",1)]
        [InlineData("Dupont", 0)]
        [InlineData("Pironet", 1)]
        public async Task FindStudentsByLastName(string search, int result)
        {
            await _dbSetup.InitStudentsDataAsync();

            // instantiate repo with NullLogger and injected connection string
            var logger = NullLogger<Repositories.StudentDapperRepo>.Instance;
            var repo = new Repositories.StudentDapperRepo(logger, _connectionString);

            // act
            var students = repo.GetByLastName(search);

            Assert.Equal(result, students.Count);

        }
    }
}
