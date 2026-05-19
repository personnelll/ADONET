using DTO;
using Microsoft.Extensions.Logging.Abstractions;
using Repositories;
using Shared;
namespace Tests.RepositoriesTests
{
    [Collection("IntegrationDB")]
    public class KotRepoTest
    {
        private readonly DatabaseFixture _fixture;
        private string _connectionString;
        DBSetup _dbSetup => _fixture.DbSetup;

        public KotRepoTest(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _connectionString = fixture.ConnectionString;

        }

        [Fact]
        public async Task GetAllTest()
        {
            await _dbSetup.InitKotsDataAsync();

            // instantiate repo with NullLogger and injected connection string
            var logger = NullLogger<KotRepo>.Instance;
            var repo = new KotRepo(logger, _connectionString);

            //act
            var kots = repo.GetAll();

            //Assert
            Assert.NotNull(kots);
            Assert.NotEmpty(kots);
            Assert.Equal(2, kots.Count);            
        }

        [Fact]
        public async Task DeleteTest()
        {
            await _dbSetup.InitKotsDataAsync();
            // instantiate repo with NullLogger and injected connection string
            var logger = NullLogger<Repositories.KotRepo>.Instance;
            var repo = new Repositories.KotRepo(logger, _connectionString);

            // act
            repo.Delete(1);
            // assert

            List<KotStudentDTO> kots = repo.GetAll();

            var kot = kots.FirstOrDefault(x => x.KOT_id == 1);

            Assert.Null(kot);
        }
    }
}
