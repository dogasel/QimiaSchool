using MediatR;
using QimiaSchool.Business.Abstracts;
using QimiaSchool.Business.Implementations.Commands.Courses;
using QimiaSchool.Business.Implementations.Events.Courses;
using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.MessageBroker.Abstractions;
using static MassTransit.Logging.OperationName;

namespace QimiaSchool.Business.Implementations.Handlers.Courses.Commands;
public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, int>
{
    private readonly ICourseManager _CourseManager;
    private readonly IEventBus _eventBus;

    public UpdateCourseCommandHandler(
        ICourseManager courseManager,
        IEventBus eventBus)
    {
        _CourseManager = courseManager;
        _eventBus = eventBus;
    }
    public UpdateCourseCommandHandler(ICourseManager CourseManager)
    {
        _CourseManager = CourseManager;
    }

    public async Task<int> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var Course = new Course
        {
   
            Title = request.UpdateCourseDto.Title,
            Credits = request.UpdateCourseDto.Credits,
        };

        await _CourseManager.UpdateCourseAsync(request.Id, Course, cancellationToken);
        await _eventBus.PublishAsync(new CourseUpdateEvent
        {
            CourseId = Course.ID,
            Title = Course.Title,
            Credits = Course.Credits,
        });

        return Course.ID;
    }
}