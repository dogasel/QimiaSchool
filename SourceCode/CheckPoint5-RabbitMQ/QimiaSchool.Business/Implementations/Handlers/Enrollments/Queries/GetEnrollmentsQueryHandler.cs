using AutoMapper;
using MediatR;
using QimiaSchool.Business.Abstracts;
using QimiaSchool.Business.Implementations.Queries.Enrollments.Dtos;
using QimiaSchool.Business.Implementations.Queries.Enrollments;

public class GetEnrollmentsQueryHandler : IRequestHandler<GetEnrollmentsQuery, List<EnrollmentDto>>
{

    private readonly IEnrollmentManager _enrollmentManager;
    private readonly IMapper _mapper;

    public GetEnrollmentsQueryHandler(
        IEnrollmentManager enrollmentManager,
        IMapper mapper)
    {
        _enrollmentManager = enrollmentManager;
        _mapper = mapper;
    }

    public async Task<List<EnrollmentDto>> Handle(GetEnrollmentsQuery request, CancellationToken cancellationToken)
    {
        var enrollments = await _enrollmentManager.GetEnrollmentsAsync(cancellationToken);

        return _mapper.Map<List<EnrollmentDto>>(enrollments);
    }
}