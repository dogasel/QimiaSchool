using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using QimiaSchool.Business.Implementations.Queries.Student.Dtos;
using QimiaSchool.DataAccess.Entities;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace QimiaSchool.IntegrationTests
{
    internal class GetStudentsTests : IntegrationTestBase
    {
        public GetStudentsTests() : base()
        {
        }

        [Test]
        public async Task GetStudents_WhenCalled_ReturnsAllStudents()
        {
            // Arrange
            var studentList = new List<Student>()
            {
                new Student
                {
                    EnrollmentDate = DateTime.Now,
                    FirstMidName = "Test1",
                    LastName = "Test1",
                },
                new Student
                {
                    EnrollmentDate = DateTime.Now,
                    FirstMidName = "Test2",
                    LastName = "Test2",
                }
            };

            databaseContext.Students.AddRange(studentList);
            await databaseContext.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/students");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var responseStudents = JsonConvert.DeserializeObject<List<StudentDto>>(result);

            // Assert
            responseStudents.Should().BeEquivalentTo(studentList, options => options
                .Excluding(s => s.EnrollmentDate)
                .Excluding(s => s.Enrollments));
        }
    }
}
