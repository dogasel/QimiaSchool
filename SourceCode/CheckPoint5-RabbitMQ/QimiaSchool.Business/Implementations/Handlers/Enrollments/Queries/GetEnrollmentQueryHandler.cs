using AutoMapper;
using MediatR;
using QimiaSchool.Business.Abstracts;
using QimiaSchool.Business.Implementations.Queries.Enrollments.Dtos;
using QimiaSchool.Business.Implementations.Queries.Enrollments;
using QimiaSchool.Business.Implementations;
using QimiaSchool.DataAccess.MessageBroker.Abstractions;
using QimiaSchool.Business.Implementations.Events.Enrollments;

public class GetEnrollmentQueryHandler : IRequestHandler<GetEnrollmentQuery, EnrollmentDto>
{
    private readonly IEnrollmentManager _enrollmentManager;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;

    public GetEnrollmentQueryHandler(
        IEnrollmentManager enrollmentManager,
        IEventBus eventBus)
    {
        _enrollmentManager = enrollmentManager;
        _eventBus = eventBus;
    }

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
        await _eventBus.PublishAsync(new EnrollmentGetByIdEvent
        {
            EnrollmentId = request.Id,

        });

        return _mapper.Map<EnrollmentDto>(enrollment);
    }
}