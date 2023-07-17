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
    private readonly ICacheService _cacheService;
    
    public CourseManager(ICourseRepository courseRepository, ILogger courseLogger, ICacheService cacheService)
    {
        _courseRepository = courseRepository;
        _courseLogger = courseLogger;
        _cacheService = cacheService;
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

    public async Task<Course> GetCourseByIdAsync(int courseId, CancellationToken cancellationToken)
    {
        var cacheKey = $"course-{courseId}";
        var cachedCourse = await _cacheService.GetAsync<Course>(cacheKey, cancellationToken);
        if (cachedCourse != null)
        {
            return cachedCourse;
        }

        var course = await _courseRepository.GetByIdAsync(courseId, cancellationToken);
        await _cacheService.SetAsync(cacheKey, course, TimeSpan.FromMinutes(5), cancellationToken);

        return course;
    }

    public async Task DeleteCourseAsync(int courseId, CancellationToken cancellationToken)
    {
        var cacheKey = $"course-{courseId}";
        var cachedCourse = await _cacheService.GetAsync<Course>(cacheKey, cancellationToken);
        if (cachedCourse != null)
        {
            await _cacheService.RemoveAsync(cacheKey, cancellationToken);
        }

        _courseLogger.Information(
              "Delete course request is accepted. Course:{@course}",
              courseId);
        await _courseRepository.DeleteAsync(courseId, cancellationToken);
    }


    public async Task UpdateCourseAsync(int CourseId, Course course, CancellationToken cancellationToken)
    {
        var cacheKey = $"course-{course.ID}";
        var cachedCourse = await _cacheService.GetAsync<Course>(cacheKey, cancellationToken);

        if (cachedCourse != null)
        {
            await _cacheService.RemoveAsync(cacheKey, cancellationToken);
        }

        await _courseRepository.UpdateAsync(course, cancellationToken);
    }


    public async Task<List<Course>> GetCoursesAsync(CancellationToken cancellationToken)
    {
        _courseLogger.Information(
           "Get courses is accepted. Courses:{@course}"
           );
        return await _courseRepository.GetAllAsync(cancellationToken);
    }
}