using MediatR;
using QimiaSchool.Business.Abstracts;
using QimiaSchool.Business.Implementations.Commands.Students;
using QimiaSchool.Business.Implementations.Events.Students;
using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.MessageBroker.Abstractions;

namespace QimiaSchool.Business.Implementations.Handlers.Students.Commands;
public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, int>
{
    private readonly IStudentManager _studentManager;
    private readonly IEventBus _eventBus;

    public UpdateStudentCommandHandler(
        IStudentManager StudentManager,
        IEventBus eventBus)
    {
        _studentManager = StudentManager;
        _eventBus = eventBus;
    }
    public UpdateStudentCommandHandler(IStudentManager studentManager)
    {
        _studentManager = studentManager;
    }

    public async Task<int> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = new Student
        {
            FirstMidName = request.Student.FirstMidName,
            LastName = request.Student.LastName,
            EnrollmentDate = DateTime.Now,
        };

        await _studentManager.UpdateStudentAsync(request.Id, student, cancellationToken);
        await _eventBus.PublishAsync(new StudentUpdateEvent
        {
            ID = student.ID,
            FirstMidName =student.FirstMidName,
            LastName = student.LastName,
            
        });

        return student.ID;
    }
}
