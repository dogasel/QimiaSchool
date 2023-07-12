using MediatR;
using QimiaSchool.Business.Abstracts;
using QimiaSchool.Business.Implementations.Commands.Enrollments;
using QimiaSchool.DataAccess.Entities;

namespace QimiaSchool.Business.Implementations.Handlers.Enrollments.Commands;
public class UpdateEnrollmentCommandHandler : IRequestHandler<UpdateEnrollmentCommand, int>
{
    private readonly IEnrollmentManager _EnrollmentManager;

    public UpdateEnrollmentCommandHandler(IEnrollmentManager EnrollmentManager)
    {
        _EnrollmentManager = EnrollmentManager;
    }

    public async Task<int> Handle(UpdateEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var Enrollment = new Enrollment
        {
            Grade = request.UpdateEnrollmentDto.Grade,
            StudentID=request.UpdateEnrollmentDto.StudentID,
            CourseID= request.UpdateEnrollmentDto.CourseID,

        };

        await _EnrollmentManager.UpdateEnrollmentAsync(request.Id, Enrollment, cancellationToken);


        return Enrollment.ID;
    }
}