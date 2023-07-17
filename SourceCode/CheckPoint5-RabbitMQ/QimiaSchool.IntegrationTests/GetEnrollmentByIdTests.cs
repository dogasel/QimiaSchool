using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using QimiaSchool.Business.Implementations.Queries.Enrollments.Dtos;
using QimiaSchool.DataAccess.Entities;
using System.Net;
using System;
using System.Collections.Generic;//These directives are required for the classes and types used in your code, such as List<T>, DateTime, Task, and Task<T>. Adding these directives should resolve the compilation errors.
using System.Threading.Tasks;


namespace QimiaSchool.IntegrationTests;

internal class GetEnrollmentByIdTests : IntegrationTestBase
{
    public GetEnrollmentByIdTests() : base()
    {
    }

    [Test]
    public async Task GetEnrollmentById_WhenCalled_ReturnsCorrectEnrollment()
    {
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
        // Arrange
        var EnrollmentList = new List<Enrollment>()
        {
            new ()
            {
                    StudentID=studentList[0].ID,
                     CourseID=CourseList[0].ID,
                    Grade=Grade.A
            },
            new ()
            {
                  StudentID=studentList[1].ID,
                  CourseID=CourseList[1].ID,
                  Grade=Grade.A
            }
        };

        databaseContext.Enrollments.AddRange(EnrollmentList);
        await databaseContext.SaveChangesAsync();

        // Act
        var response = await client.GetAsync("/Enrollments/" + EnrollmentList[0].ID);
        var result = await response.Content.ReadAsStringAsync();
        var responseEnrollment = JsonConvert.DeserializeObject<EnrollmentDto>(result);

        // Assert
        responseEnrollment
            .Should()
            .BeEquivalentTo(EnrollmentList[0],
                options => options
                    .Excluding(s => s.Student)
                    .Excluding(s => s.Course));
    }

    [Test]
    public async Task GetEnrollments_WhenEnrollmentISNotExist_ReturnsNotFound()
    {
        // Act
        var response = await client.GetAsync("/Enrollment/NonExistingId");

        // Assert
        response
            .StatusCode
            .Should()
            .Be(HttpStatusCode.NotFound);
    }
}
