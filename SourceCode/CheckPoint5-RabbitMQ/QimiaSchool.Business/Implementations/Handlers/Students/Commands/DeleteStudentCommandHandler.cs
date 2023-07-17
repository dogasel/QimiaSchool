using MediatR;
using QimiaSchool.Business.Abstracts;
using QimiaSchool.Business.Implementations.Commands.Students;
using QimiaSchool.Business.Implementations.Events.Students;
using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.MessageBroker.Abstractions;

namespace QimiaSchool.Business.Implementations.Handlers.Students.Commands;
public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, int>
{
    private readonly IStudentManager _studentManager;
    private readonly IEventBus _eventBus;

    public DeleteStudentCommandHandler(IStudentManager studentManager, IEventBus eventBus)
    {
        _studentManager = studentManager;
        _eventBus = eventBus;

    }
    public DeleteStudentCommandHandler(IStudentManager studentManager)
    {
        _studentManager = studentManager;
    }

    public async Task<int> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {

        await _studentManager.DeleteStudentAsync(request.Id,cancellationToken);
        await _eventBus.PublishAsync(new StudentDeleteEvent
        {
            
            ID= request.Id,
           
        });

        return request.Id;
    }
}
