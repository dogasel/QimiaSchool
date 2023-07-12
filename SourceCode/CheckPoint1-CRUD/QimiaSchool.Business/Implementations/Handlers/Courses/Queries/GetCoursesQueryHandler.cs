using AutoMapper;
using MediatR;
using QimiaSchool.Business.Abstracts;
using QimiaSchool.Business.Implementations.Queries.Courses.Dtos;
using QimiaSchool.Business.Implementations.Queries.Courses;

public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, List<CourseDto>>
{

    private readonly ICourseManager _courseManager;
    private readonly IMapper _mapper;

    public GetCoursesQueryHandler(
        ICourseManager courseManager,
        IMapper mapper)
    {
        _courseManager = courseManager;
        _mapper = mapper;
    }

    public async Task<List<CourseDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await _courseManager.GetCoursesAsync(cancellationToken);

        return _mapper.Map<List<CourseDto>>(courses);
    }
}