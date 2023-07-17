using MediatR;
using QimiaSchool.Business.Abstracts;
using QimiaSchool.Business.Implementations.Commands.Courses;
using QimiaSchool.Business.Implementations.Events.Courses;
using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.MessageBroker.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using static MassTransit.Logging.OperationName;

namespace QimiaSchool.Business.Implementations.Handlers.Courses.Commands;
public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, int>
{
    private readonly ICourseManager _CourseManager;
    private readonly IEventBus _eventBus;

    public DeleteCourseCommandHandler(
        ICourseManager courseManager,
        IEventBus eventBus)
    {
        _CourseManager = courseManager;
        _eventBus = eventBus;
    }

    public DeleteCourseCommandHandler(ICourseManager CourseManager)
    {
        _CourseManager = CourseManager;
    }

    public async Task<int> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {

        await _CourseManager.DeleteCourseAsync(request.CourseId, cancellationToken);
        await _eventBus.PublishAsync(new CourseDeleteEvent
        {
            CourseId = request.CourseId
        
        });

        return request.CourseId;
    }
}
