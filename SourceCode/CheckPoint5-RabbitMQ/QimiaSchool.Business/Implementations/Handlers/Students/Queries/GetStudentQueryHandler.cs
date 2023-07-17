using AutoMapper;
using MediatR;
using QimiaSchool.Business.Abstracts;
using QimiaSchool.Business.Implementations.Events.Students;
using QimiaSchool.Business.Implementations.Queries.Student;
using QimiaSchool.Business.Implementations.Queries.Student.Dtos;
using QimiaSchool.DataAccess.MessageBroker.Abstractions;

namespace QimiaSchool.Business.Implementations.Handlers.Students.Queries;
public class GetStudentQueryHandler : IRequestHandler<GetStudentQuery, StudentDto>
{
    private readonly IStudentManager _studentManager;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;

    public GetStudentQueryHandler(IStudentManager studentManager, IEventBus eventBus)
    {
        _studentManager = studentManager;
        _eventBus = eventBus;

    }

    public GetStudentQueryHandler(
        IStudentManager studentManager,
        IMapper mapper)
    {
        _studentManager = studentManager;
        _mapper = mapper;
    }

    public async Task<StudentDto> Handle(GetStudentQuery request, CancellationToken cancellationToken)
    {
        var student = await _studentManager.GetStudentByIdAsync(request.Id, cancellationToken);
        await _eventBus.PublishAsync(new StudentGetByIdEvent
        {
            StudentId = request.Id,

        });

        return _mapper.Map<StudentDto>(student);
    }
}
