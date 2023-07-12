using AutoMapper;
using MediatR;
using QimiaSchool.Business.Abstracts;
using QimiaSchool.Business.Implementations.Queries.Enrollments.Dtos;
using QimiaSchool.Business.Implementations.Queries.Enrollments;

public class GetEnrollmentQueryHandler : IRequestHandler<GetEnrollmentQuery, EnrollmentDto>
{
    private readonly IEnrollmentManager _enrollmentManager;
    private readonly IMapper _mapper;

    public GetEnrollmentQueryHandler(
        IEnrollmentManager enrollmentManager,
        IMapper mapper)
    {
        _enrollmentManager = enrollmentManager;
        _mapper = mapper;
    }

    public async Task<EnrollmentDto> Handle(GetEnrollmentQuery request, CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentManager.GetEnrollmentByIdAsync(request.Id, cancellationToken);

        return _mapper.Map<EnrollmentDto>(enrollment);
    }
}