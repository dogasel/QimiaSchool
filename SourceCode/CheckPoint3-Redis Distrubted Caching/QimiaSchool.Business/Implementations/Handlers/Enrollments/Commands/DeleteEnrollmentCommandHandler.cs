using MediatR;
using QimiaSchool.Business.Abstracts;
using QimiaSchool.Business.Implementations.Commands.Enrollments;
using QimiaSchool.DataAccess.Entities;

namespace QimiaSchool.Business.Implementations.Handlers.Enrollments.Commands;
public class DeleteEnrollmentCommandHandler : IRequestHandler<DeleteEnrollmentCommand, int>
{
    private readonly IEnrollmentManager _EnrollmentManager;

    public DeleteEnrollmentCommandHandler(IEnrollmentManager EnrollmentManager)
    {
        _EnrollmentManager = EnrollmentManager;
    }

    public async Task<int> Handle(DeleteEnrollmentCommand request, CancellationToken cancellationToken)
    {

        await _EnrollmentManager.DeleteEnrollmentAsync(request.Id, cancellationToken);


        return request.Id;
    }
}
