using AutoMapper;
using MediatR;
using QimiaSchool.Business.Abstracts;
using QimiaSchool.Business.Implementations.Queries.Courses.Dtos;
using QimiaSchool.Business.Implementations.Queries.Courses;
using QimiaSchool.DataAccess.MessageBroker.Abstractions;
using QimiaSchool.Business.Implementations.Events.Courses;

public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, CourseDto>
{
    private readonly ICourseManager _courseManager;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;

    public GetCourseQueryHandler(
        ICourseManager courseManager,
        IEventBus eventBus)
    {
        _courseManager = courseManager;
        _eventBus = eventBus;
    }

    public GetCourseQueryHandler(
        ICourseManager courseManager,
        IMapper mapper)
    {
        _courseManager = courseManager;
        _mapper = mapper;
    }

    public async Task<CourseDto> Handle(GetCourseQuery request, CancellationToken cancellationToken)
    {
        var course = await _courseManager.GetCourseByIdAsync(request.Id, cancellationToken);
        await _eventBus.PublishAsync(new CourseGetByIdEvent
        {
            CourseId = request.Id,
          
        });
        return _mapper.Map<CourseDto>(course);
    }
}