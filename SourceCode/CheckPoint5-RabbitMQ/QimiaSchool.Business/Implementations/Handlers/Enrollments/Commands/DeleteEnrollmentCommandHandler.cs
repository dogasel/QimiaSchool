using MediatR;
using QimiaSchool.Business.Abstracts;
using QimiaSchool.Business.Implementations.Commands.Enrollments;
using QimiaSchool.Business.Implementations.Events.Enrollments;
using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.MessageBroker.Abstractions;
using static MassTransit.Logging.OperationName;

namespace QimiaSchool.Business.Implementations.Handlers.Enrollments.Commands;
public class DeleteEnrollmentCommandHandler : IRequestHandler<DeleteEnrollmentCommand, int>
{
    private readonly IEnrollmentManager _EnrollmentManager;
    private readonly IEventBus _eventBus;

    public DeleteEnrollmentCommandHandler(IEnrollmentManager enrollmentManager, IEventBus eventBus)
    {
        _EnrollmentManager = enrollmentManager;
        _eventBus = eventBus;

    }
    public DeleteEnrollmentCommandHandler(IEnrollmentManager EnrollmentManager)
    {
        _EnrollmentManager = EnrollmentManager;
    }

    public async Task<int> Handle(DeleteEnrollmentCommand request, CancellationToken cancellationToken)
    {

        await _EnrollmentManager.DeleteEnrollmentAsync(request.Id, cancellationToken);
        await _eventBus.PublishAsync(new EnrollmentCreatedEvent
        {
            Id = request.Id,
            
        });
        return request.Id;
    }
}
