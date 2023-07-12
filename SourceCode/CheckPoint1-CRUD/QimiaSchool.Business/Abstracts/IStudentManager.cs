using QimiaSchool.DataAccess.Entities;

namespace QimiaSchool.Business.Abstracts;

public interface IStudentManager
{
    public Task CreateStudentAsync(
        Student student,
        CancellationToken cancellationToken);

    public Task<Student> GetStudentByIdAsync(
        int studentId,
        CancellationToken cancellationToken);
    public Task<List<Student>> GetStudentsAsync(
  
        CancellationToken cancellationToken);
    public Task UpdateStudentAsync(
        int studentId,
        Student student,
        CancellationToken cancellationToken);
    public Task DeleteStudentAsync(
       int studentId,
       CancellationToken cancellationToken);

}
