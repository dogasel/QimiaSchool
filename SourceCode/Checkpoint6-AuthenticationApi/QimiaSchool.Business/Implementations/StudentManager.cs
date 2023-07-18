using QimiaSchool.Business.Abstracts;
using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.Repositories.Abstractions;
using Serilog;

namespace QimiaSchool.Business.Implementations;

public class StudentManager : IStudentManager
{
    private readonly IStudentRepository _studentRepository;
    private readonly Serilog.ILogger _studentLogger;
    private readonly ICacheService _cacheService;
    public StudentManager(IStudentRepository studentRepository, ILogger studentLogger, ICacheService cacheService)
    {
        _studentRepository = studentRepository;
        _studentLogger = studentLogger;
        _cacheService = cacheService;
    }

    public Task CreateStudentAsync(

        Student student,
        CancellationToken cancellationToken)
    {
        // Serilog
        // Serilog with context
        _studentLogger.Information(
            "Create student request is accepted. Student:{@student}",
            student);


        // No id should be provided while insert.
        student.ID = default;

        return _studentRepository.CreateAsync(student, cancellationToken);
    }

    

    public async Task<List<Student>> GetStudentsAsync(CancellationToken cancellationToken)
    {
        _studentLogger.Information(
            "Get students  request is accepted. Students:{@student}"
            );
        return await _studentRepository.GetAllAsync(cancellationToken);
    }
    public async Task UpdateStudentAsync(int StudentId, Student student, CancellationToken cancellationToken)
    {
        var cacheKey = $"student-{student.ID}";
        var cachedStudent = await _cacheService.GetAsync<Student>(cacheKey, cancellationToken);

        if (cachedStudent != null)
        {
            await _cacheService.RemoveAsync(cacheKey, cancellationToken);
        }

        await _studentRepository.UpdateAsync(student, cancellationToken);
    }

    public async Task DeleteStudentAsync(int studentId, CancellationToken cancellationToken)
    {
        var cacheKey = $"student-{studentId}";
        var cachedStudent = await _cacheService.GetAsync<Student>(cacheKey, cancellationToken);
        if (cachedStudent != null)
        {
            await _cacheService.RemoveAsync(cacheKey, cancellationToken);
        }
        await _studentRepository.DeleteAsync(studentId, cancellationToken);
    }

    public async Task<Student> GetStudentByIdAsync(int studentId, CancellationToken cancellationToken)
    {
        var cacheKey = $"student-{studentId}";
        var cachedStudent = await _cacheService.GetAsync<Student>(cacheKey, cancellationToken);
        if (cachedStudent != null)
        {
            return cachedStudent;
        }

        var student = await _studentRepository.GetByIdAsync(studentId, cancellationToken);
        if (student != null)
        {
            await _cacheService.SetAsync(cacheKey, student, TimeSpan.FromMinutes(5), cancellationToken);
        }

        return student;
    }

}
