using MassTransit;
using Microsoft.Extensions.Logging;
using QimiaSchool.Business.Abstracts;
using QimiaSchool.Business.Implementations.Events.Courses;

namespace QimiaSchool.Business.Implementations.Events.Students;
public class StudentCreatedEventConsumer : IConsumer<StudentCreatedEvent>
{
    private readonly ILogger<StudentCreatedEventConsumer> _logger;

    public StudentCreatedEventConsumer(ILogger<StudentCreatedEventConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<StudentCreatedEvent> context)
    {
        _logger.LogInformation("Student created: {@Student}", context.Message);

        return Task.CompletedTask;
    }
}
