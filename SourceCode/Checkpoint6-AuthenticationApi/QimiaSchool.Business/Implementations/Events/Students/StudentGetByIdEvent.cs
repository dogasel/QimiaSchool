using MassTransit;
using Microsoft.Extensions.Logging;
using QimiaSchool.DataAccess.Entities;



namespace QimiaSchool.Business.Implementations.Events.Students
{
    public class StudentGetByIdEvent
    {
        public int StudentId { get; set; }
    }
}