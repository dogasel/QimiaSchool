using Moq;
using QimiaSchool.Business.Abstracts;
using QimiaSchool.Business.Implementations;
using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.Repositories.Abstractions;
using Serilog;

[TestFixture]
internal class CourseManagerUnitTests
{
    private readonly Mock<ICourseRepository> _mockCourseRepository;
    private readonly Mock<ILogger> _mockCourseLogger;
    private readonly CourseManager _courseManager;
    private readonly Mock<ICacheService> _mockCacheService;

    public CourseManagerUnitTests()
    {
        _mockCourseRepository = new Mock<ICourseRepository>();
        _mockCourseLogger = new Mock<ILogger>();
        _mockCacheService = new Mock<ICacheService>();
        _courseManager = new CourseManager(
            _mockCourseRepository.Object,
            _mockCourseLogger.Object,
            _mockCacheService.Object
        );
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
        await _courseManager.CreateCourseAsync(testCourse, default);

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
            Credits = 2,
            Title = "Test"
        };

        // Act
        await _courseManager.CreateCourseAsync(testCourse, default);

        // Assert
        _mockCourseRepository
            .Verify(
                sr => sr.CreateAsync(
                    It.Is<Course>(s => s == testCourse),
                    It.IsAny<CancellationToken>()), Times.Once);
    }
}
