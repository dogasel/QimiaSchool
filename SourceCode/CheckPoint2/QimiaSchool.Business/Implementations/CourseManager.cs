using QimiaSchool.Business.Abstracts;
using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.Repositories.Abstractions;
using QimiaSchool.DataAccess.Repositories.Implementations;
using Serilog;
namespace QimiaSchool.Business.Implementations;

public class CourseManager : ICourseManager
{
    private readonly ICourseRepository _courseRepository;
    private readonly Serilog.ILogger _courseLogger;
    public CourseManager(ICourseRepository courseRepository, ILogger courseLogger)
    {
        _courseRepository = courseRepository;
        _courseLogger = courseLogger;

    }

    public Task CreateCourseAsync(
        Course course,
        CancellationToken cancellationToken)
    {
        // Serilog
        // Serilog with context
        _courseLogger.Information(
            "Create course request is accepted. Course:{@course}",
            course);
        // No id should be provided while insert.
        course.ID = default;

        return _courseRepository.CreateAsync(course, cancellationToken);
    }
    
    public Task<Course> GetCourseByIdAsync(
        int courseId,
        CancellationToken cancellationToken)
    {
        return _courseRepository.GetByIdAsync(courseId, cancellationToken);
    }
    public Task DeleteCourseAsync(int courseId, CancellationToken cancellationToken)
    {
        return _courseRepository.DeleteAsync(courseId, cancellationToken);
    }


    public Task UpdateCourseAsync(int courseId, Course course, CancellationToken cancellationToken)
    {
        course.ID = courseId;

        return _courseRepository.UpdateAsync(course, cancellationToken);
    }

    public async Task<List<Course>> GetCoursesAsync(CancellationToken cancellationToken)
    {
        return await _courseRepository.GetAllAsync(cancellationToken);
    }
}