using QimiaSchool.DataAccess.Entities;

namespace QimiaSchool.Business.Implementations.Events.Courses;

public record StudentCreatedEvent
{
    public int ID { get; init; }
    public string LastName { get; init; } = string.Empty;
    public string FirstMidName { get; init; } = string.Empty;

}
