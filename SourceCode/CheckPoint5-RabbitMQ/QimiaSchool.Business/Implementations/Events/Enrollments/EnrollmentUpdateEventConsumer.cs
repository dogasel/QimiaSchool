using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Events.Enrollments
{
    internal class EnrollmentUpdateEventConsumer : IConsumer<EnrollmentUpdateEvent>
    {
        private readonly ILogger<EnrollmentUpdateEventConsumer> _logger;

        public EnrollmentUpdateEventConsumer(ILogger<EnrollmentUpdateEventConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<EnrollmentUpdateEvent> context)
        {
            _logger.LogInformation("Enrollment updated: {@Enrollment}", context.Message);

            return Task.CompletedTask;
        }
    }
}