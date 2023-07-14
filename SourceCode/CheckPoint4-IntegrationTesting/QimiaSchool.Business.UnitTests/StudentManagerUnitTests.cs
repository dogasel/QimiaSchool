using Moq;
using QimiaSchool.Business.Abstracts;
using QimiaSchool.Business.Implementations;
using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.Repositories.Abstractions;
using Serilog;
namespace QimiaSchool.Business.UnitTests;

[TestFixture]
internal class StudentManagerUnitTests
{
    private readonly Mock<IStudentRepository> _mockStudentRepository;
    private readonly StudentManager _studentManager;
    private readonly Mock<ICacheService> _mockCacheService;
    private readonly Mock<ILogger> _mockStudentLogger;

    public StudentManagerUnitTests()
    {
        _mockStudentRepository = new Mock<IStudentRepository>();
        _mockCacheService = new Mock<ICacheService>();
        _mockStudentLogger = new Mock<ILogger>();
        _studentManager = new StudentManager(
            _mockStudentRepository.Object,
            _mockStudentLogger.Object,
            _mockCacheService.Object
        );
    }

    [Test]
    public async Task CreateStudentAsync_WhenCalled_CallsRepository()
    {
        // Arrange
        var testStudent = new Student
        {
            EnrollmentDate = DateTime.Now,
            FirstMidName = "Test",
            LastName = "Test"
        };

        // Act
        await _studentManager.CreateStudentAsync(testStudent, default);

        // Assert
        _mockStudentRepository
            .Verify(
                sr => sr.CreateAsync(
                    It.Is<Student>(s => s == testStudent),
                    It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task CreateStudentAsync_WhenStudentIdHasValue_RemovesAndCallsRepository()
    {
        // Arrange
        var testStudent = new Student
        {
            ID = 1,
            EnrollmentDate = DateTime.Now,
            FirstMidName = "Test",
            LastName = "Test"
        };

        // Act
        await _studentManager.CreateStudentAsync(testStudent, default);

        // Assert
        _mockStudentRepository
            .Verify(
                sr => sr.CreateAsync(
                    It.Is<Student>(s => s == testStudent),
                    It.IsAny<CancellationToken>()), Times.Once);
    }
}

