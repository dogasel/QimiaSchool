using QimiaSchool.Business.Abstracts;
using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.Repositories.Abstractions;
using QimiaSchool.DataAccess.Repositories.Implementations;
using Serilog;
namespace QimiaSchool.Business.Implementations;

public class EnrollmentManager : IEnrollmentManager 
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly Serilog.ILogger _enrollmentLogger;
    public EnrollmentManager(IEnrollmentRepository enrollmentRepository, ILogger enrollmentLogger)
    {
        _enrollmentRepository = enrollmentRepository;
        _enrollmentLogger = enrollmentLogger;

    }

    public Task CreateEnrollmentAsync(
        Enrollment enrollment,
        CancellationToken cancellationToken)
    {
        _enrollmentLogger.Information(
            "Create enrollment request is accepted. Enrollment:{@enrollment}",
            enrollment);

        // No id should be provided while insert.
        enrollment.ID = default;

        return _enrollmentRepository.CreateAsync(enrollment, cancellationToken);
    }

    public Task<Enrollment> GetEnrollmentByIdAsync(int enrollmentId, CancellationToken cancellationToken)
    {
        _enrollmentLogger.Information(
           "Get enrollment by Id request is accepted. Id :{@enrollmentId}",
           enrollmentId);
        return _enrollmentRepository.GetByIdAsync(enrollmentId, cancellationToken);
    }

    public Task UpdateEnrollmentAsync(int enrollmentId, Enrollment enrollment, CancellationToken cancellationToken)
    {
        _enrollmentLogger.Information(
           "Update enrollmen request is accepted. Enrollment:{@enrollment}",
           enrollmentId);
        enrollment.ID = enrollmentId;

        return _enrollmentRepository.UpdateAsync(enrollment, cancellationToken);
    }
    public Task DeleteEnrollmentAsync(int enrollmentId, CancellationToken cancellationToken)
    {
        _enrollmentLogger.Information(
                    "Delete enrollment request is accepted. Enrollment:{@enrollment}",
                    enrollmentId);
        return _enrollmentRepository.DeleteAsync(enrollmentId, cancellationToken);
    }

    public async Task<List<Enrollment>> GetEnrollmentsAsync(CancellationToken cancellationToken)
    {
        _enrollmentLogger.Information(
           "Get enrollments is accepted. Enrollment:{@enrollment}"
           );
        return await _enrollmentRepository.GetAllAsync(cancellationToken);
    }
}