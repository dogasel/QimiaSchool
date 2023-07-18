using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Events.Courses
{
    internal class CourseUpdateEventConsumer : IConsumer<CourseUpdateEvent>
    {
        private readonly ILogger<CourseUpdateEventConsumer> _logger;

        public CourseUpdateEventConsumer(ILogger<CourseUpdateEventConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<CourseUpdateEvent> context)
        {
            _logger.LogInformation("Course updated: {@Course}", context.Message);

            return Task.CompletedTask;
        }
    }
}