using QimiaSchool.Business.Implementations.Queries.Enrollments.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Queries.Courses.Dtos
{
    public class CourseDto
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; }

        public ICollection<EnrollmentDto>? Enrollments
        {
            get; set;
        }
    }
}
