using MediatR;
using QimiaSchool.Business.Abstracts;
using QimiaSchool.Business.Implementations.Commands.Courses;
using QimiaSchool.DataAccess.Entities;

namespace QimiaSchool.Business.Implementations.Handlers.Courses.Commands;
public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, int>
{
    private readonly ICourseManager _CourseManager;

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


        return Course.ID;
    }
}