using MediatR;
using QimiaSchool.Business.Abstracts;
using QimiaSchool.Business.Implementations.Commands.Courses;
using QimiaSchool.DataAccess.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Handlers.Courses.Commands;
public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, int>
{
    private readonly ICourseManager _CourseManager;

    public DeleteCourseCommandHandler(ICourseManager CourseManager)
    {
        _CourseManager = CourseManager;
    }

    public async Task<int> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {

        await _CourseManager.DeleteCourseAsync(request.CourseId, cancellationToken);


        return request.CourseId;
    }
}
