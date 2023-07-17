using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using QimiaSchool.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.IntegrationTests;
internal class UpdateCourseTests : IntegrationTestBase
{
    public UpdateCourseTests() : base()
    { }

    [Test]
    public async Task UpdateCourse_WhenCalled_ReturnsCorrectUpdatedCourse()
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

        CourseList[0].Credits = 0;
        CourseList[0].Title = "TestUpdate";

        var json = JsonConvert.SerializeObject(CourseList[0]);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        await client.PutAsync("/Courses/" + CourseList[0].ID, content);

        var updatedCourse = await databaseContext.Courses.FindAsync(CourseList[0].ID);

        // Assert
        updatedCourse.Credits.Should().Be(0);
        updatedCourse.Title.Should().Be("TestUpdate");
    }

}