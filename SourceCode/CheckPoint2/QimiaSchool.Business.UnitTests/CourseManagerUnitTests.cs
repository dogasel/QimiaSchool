using Moq;
using QimiaSchool.Business.Implementations;
using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.Repositories.Abstractions;
using Serilog;
namespace QimiaSchool.Business.UnitTests;

[TestFixture]
internal class CourseManagerUnitTests
{
    private readonly Mock<ICourseRepository> _mockCourseRepository;
    private readonly ILogger _CourseLogger;
    private readonly CourseManager _CourseManager;

    public CourseManagerUnitTests()
    {
        _mockCourseRepository = new Mock<ICourseRepository>();
        _CourseManager = new CourseManager(_mockCourseRepository.Object, _CourseLogger);
    }

    [Test]
    public async Task CreateCourseAsync_WhenCalled_CallsRepository()
    {
        // Arrange
        var testCourse = new Course
        {
            ID = 1,
            Credits = 2,
            Title = "Test"
        };

        // Act
        await _CourseManager.CreateCourseAsync(testCourse, default);

        // Assert
        _mockCourseRepository
            .Verify(
                sr => sr.CreateAsync(
                    It.Is<Course>(s => s == testCourse),
                    It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task CreateCourseAsync_WhenCourseIdHasValue_RemovesAndCallsRepository()
    {
        // Arrange
        var testCourse = new Course
        {
            ID = 1,
            Credits= 2,
            Title= "Test"
        };

        // Act
        await _CourseManager.CreateCourseAsync(testCourse, default);

        // Assert
        _mockCourseRepository
            .Verify(
                sr => sr.CreateAsync(
                    It.Is<Course>(s => s == testCourse),
                    It.IsAny<CancellationToken>()), Times.Once);
    }
}
