using MediatR;

namespace QimiaSchool.Business.Implementations.Commands.Courses
{
    public class DeleteCourseCommand : IRequest<int>
    {
        public int CourseId { get; set; }

        public DeleteCourseCommand(int courseId)
        {
            CourseId = courseId;
        }
    }
}
