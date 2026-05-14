using Interfaces;
using Models;
using ServicesDLL;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.services
{
    public class StudentServiceTest
    {
        Mock<IStudentRepo> _mockRepo;
        NullLogger<StudentsService> _logger;
        IStudentsService _service;
        public StudentServiceTest()
        {
            _mockRepo = new Mock<IStudentRepo>();
            _logger = NullLogger<StudentsService>.Instance;
            _service = new StudentsService(_logger, _mockRepo.Object);

        }
        [Fact]
        public void GetAll_ReturnsAllStudents()
        {
            var students = new List<Students>
            {
                new Students { Id = 1, matricule = "M001", firstName = "John", lastName = "Doe" },
                new Students { Id = 2, matricule = "M002", firstName = "Jane", lastName = "Smith" }
            };

            _mockRepo.Setup(r => r.GetAll()).Returns(students);

            //Act
            var result = _service.GetAll();

            //Assert
            Assert.Equal(students.Count, result.Count);
            Assert.Equal(students[0].matricule, result[0].matricule);
            Assert.Equal(students[1].lastName, result[1].lastName);
            _mockRepo.Verify(r => r.GetAll(), Times.Once);


        }

        [Fact]
        public void GetAll_ReturnsNoStudents()
        {
            var students = new List<Students>();

            _mockRepo.Setup(r => r.GetAll()).Returns(students);


            //Act
            var result = _service.GetAll();

            //Assert
            Assert.Equal(students.Count, result.Count);
            _mockRepo.Verify(r => r.GetAll(), Times.Once);


        }

        [Fact]
        public void AddWithValidData()
        {
            // Arrange
            var student = new Students
            {
                lastName = "Dupont",
                firstName = "Jean",
                matricule = "HE01"
            };

            // Act
            _service.Add(student);

            // Assert
            _mockRepo.Verify(r => r.Add(It.Is<Students>(s => s == student)), Times.Once);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("xx123")]

        public void AddWithInvalidMatriculeAndThrowsArgumentException(string? matricule)
        {
            // Arrange
            var student = new Students
            {
                lastName = "Dupont",
                firstName = "Jean",
                matricule = matricule
            };

            Assert.Throws<ArgumentException>(() => _service.Add(student));
            _mockRepo.Verify(r => r.Add(student), Times.Never);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]

        public void AddWithInvalidLastNameAndThrowsArgumentException(string? lastName)
        {
            var student = new Students
            {
                lastName = lastName,
                firstName = "Jean",
                matricule = "HE01"
            };
            Assert.Throws<ArgumentException>(() => _service.Add(student));
            _mockRepo.Verify(r => r.Add(student), Times.Never);
        }

        [Fact]
        public void AddWhenRepoThrowsExceptionPropagatesException()
        {
            //Arrange
            var student = new Students
            {
                lastName = "Dupont",
                firstName = "Jean",
                matricule = "HE01"
            };
            // Simule une exception lors de l'appel à Add
            _mockRepo.Setup(r => r.Add(It.IsAny<Students>())).Throws(new InvalidOperationException("Database error"));

            //Act && Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _service.Add(student));
            Assert.Equal("Database error", exception.Message);

            _mockRepo.Verify(r => r.Add(student), Times.Once);
        }
    }
}
