using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using QimiaSchool.Business.Implementations.Queries.Student.Dtos;
using QimiaSchool.DataAccess.Entities;
using System.Net;

namespace QimiaSchool.IntegrationTests;

internal class DeleteStudentTests : IntegrationTestBase
{
    public DeleteStudentTests() : base()
    {
    }

    [Test]
    public async Task DeleteStudent_WhenCalled_ReturnsCorrectStudent()
    {
        // Arrange
        var studentList = new List<Student>()
        {
            new ()
            {
                EnrollmentDate = DateTime.Now,
                FirstMidName = "Test",
                LastName = "Test",
            },
            new ()
            {
                EnrollmentDate = DateTime.Now,
                FirstMidName = "Test",
                LastName = "Test",
            }
        };

        databaseContext.Students.AddRange(studentList);
        await databaseContext.SaveChangesAsync();

        // Act
        var response = await client.DeleteAsync("/students/" + studentList[0].ID);
        var response2 = await client.GetAsync("/students/" + studentList[0].ID);
        

        // Assert error message or status code 500
        var contentString = await response2.Content.ReadAsStringAsync();
        contentString.Should().Contain("QimiaSchool.DataAccess.Exceptions.EntityNotFoundException");


        // Assert
        response
            .StatusCode
            .Should()
            .Be(HttpStatusCode.NoContent);//204


        // Assert

    }

   [Test]

    public async Task DeleteStudents_WhenStudentISNotExist_ReturnsNotFound()
    {
        // Act
        var response = await client.DeleteAsync("/student/NonExistingId");

        // Assert
        response
            .StatusCode
            .Should()
            .Be(HttpStatusCode.NotFound);
    }

}
