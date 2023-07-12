using Moq;
using QimiaSchool.Business.Implementations;
using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.Repositories.Abstractions;

namespace QimiaSchool.Business.UnitTests;

[TestFixture]
internal class EnrollmentManagerUnitTests
{
    private readonly Mock<IEnrollmentRepository> _mockEnrollmentRepository;
    private readonly EnrollmentManager _EnrollmentManager;

    public EnrollmentManagerUnitTests()
    {
        _mockEnrollmentRepository = new Mock<IEnrollmentRepository>();
        _EnrollmentManager = new EnrollmentManager(_mockEnrollmentRepository.Object);
    }

    [Test]
    public async Task CreateEnrollmentAsync_WhenCalled_CallsRepository()
    {
        // Arrange
        var testEnrollment = new Enrollment
        {
            StudentID=1,
            CourseID=2,
            Grade=Grade.A
        };

        // Act
        await _EnrollmentManager.CreateEnrollmentAsync(testEnrollment, default);

        // Assert
        _mockEnrollmentRepository
            .Verify(
                sr => sr.CreateAsync(
                    It.Is<Enrollment>(s => s == testEnrollment),
                    It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task CreateEnrollmentAsync_WhenEnrollmentIdHasValue_RemovesAndCallsRepository()
    {
        // Arrange
        var testEnrollment = new Enrollment
        {
            
            StudentID=1,
            CourseID=2,
            Grade=Grade.B
        };

        // Act
        await _EnrollmentManager.CreateEnrollmentAsync(testEnrollment, default);

        // Assert
        _mockEnrollmentRepository
            .Verify(
                sr => sr.CreateAsync(
                    It.Is<Enrollment>(s => s == testEnrollment),
                    It.IsAny<CancellationToken>()), Times.Once);
    }
}

