using QimiaSchool.Business.Abstracts;
using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.Repositories.Abstractions;
using Serilog;

namespace QimiaSchool.Business.Implementations;

public class StudentManager : IStudentManager
{
    private readonly IStudentRepository _studentRepository;
    private readonly Serilog.ILogger _studentLogger;
    public StudentManager(IStudentRepository studentRepository, ILogger studentLogger)
    {
        _studentRepository = studentRepository;
        _studentLogger = studentLogger;

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

    public Task<Student> GetStudentByIdAsync(
        int studentId,
        CancellationToken cancellationToken)
    {
        return _studentRepository.GetByIdAsync(studentId, cancellationToken);
    }

    public async Task<List<Student>> GetStudentsAsync(CancellationToken cancellationToken)
    {
        return await _studentRepository.GetAllAsync(cancellationToken);
    }
    public Task UpdateStudentAsync(
        int studentId,
        Student student,
        CancellationToken cancellationToken)
    {
        // No id should be provided while insert.
        student.ID = studentId;

        return _studentRepository.UpdateAsync(student, cancellationToken);
    }
    public Task DeleteStudentAsync(
        int studentId,
        
        CancellationToken cancellationToken)
    {
      
        return _studentRepository.DeleteAsync( studentId, cancellationToken);
    }
}
