using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using QimiaSchool.Business.Implementations.Queries.Courses.Dtos;
using QimiaSchool.DataAccess.Entities;
using System.Net;

namespace QimiaSchool.IntegrationTests;

internal class GetCourseByIdTests : IntegrationTestBase
{
    public GetCourseByIdTests() : base()
    {
    }

    [Test]
    public async Task GetCourseById_WhenCalled_ReturnsCorrectCourse()
    {
        // Arrange
        var CourseList = new List<Course>()
        {
            new ()
            {
                Credits = 2,
                Title = "Test"
            },
            new ()
            {
                Credits = 2,
                Title = "Test"
            }
        };

        databaseContext.Courses.AddRange(CourseList);
        await databaseContext.SaveChangesAsync();

        // Act
        var response = await client.GetAsync("/Courses/" + CourseList[0].ID);
        var result = await response.Content.ReadAsStringAsync();
        Console.WriteLine(result);

        var responseCourse = JsonConvert.DeserializeObject<CourseDto>(result);

        // Assert
        responseCourse
            .Should()
            .BeEquivalentTo(CourseList[0],
                options => options
                  
                    .Excluding(s => s.Enrollments));
    }

    [Test]
    public async Task GetCourses_WhenCourseISNotExist_ReturnsNotFound()
    {
        // Act
        var response = await client.GetAsync("/Course/NonExistingId");

        // Assert
        response
            .StatusCode
            .Should()
            .Be(HttpStatusCode.NotFound);
    }
}
