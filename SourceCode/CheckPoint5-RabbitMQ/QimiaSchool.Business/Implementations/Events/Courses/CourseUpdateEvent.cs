using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Events.Courses
{
    public class CourseUpdateEvent
    {
        public int CourseId { get; set; }
        public string Title { get; init; }
        public int Credits { get; set; }
    }
}