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
internal class UpdateStudentTests : IntegrationTestBase
{
    public UpdateStudentTests() : base()
    { }

    [Test]
    public async Task UpdateStudent_WhenCalled_ReturnsCorrectUpdatedStudent()
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

        studentList[0].FirstMidName = "TestUpdate";
        studentList[0].LastName = "TestUpdate";

        var json = JsonConvert.SerializeObject(studentList[0]);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        await client.PutAsync("/students/" + studentList[0].ID, content);

        var updatedStudent = await databaseContext.Students.FindAsync(studentList[0].ID);

        // Assert
        updatedStudent.FirstMidName.Should().Be("TestUpdate");
        updatedStudent.LastName.Should().Be("TestUpdate");
    }

}