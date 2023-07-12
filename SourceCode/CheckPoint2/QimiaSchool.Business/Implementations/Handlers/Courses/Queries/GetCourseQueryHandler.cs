using AutoMapper;
using MediatR;
using QimiaSchool.Business.Abstracts;
using QimiaSchool.Business.Implementations.Queries.Courses.Dtos;
using QimiaSchool.Business.Implementations.Queries.Courses;

public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, CourseDto>
{
    private readonly ICourseManager _courseManager;
    private readonly IMapper _mapper;

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

        return _mapper.Map<CourseDto>(course);
    }
}