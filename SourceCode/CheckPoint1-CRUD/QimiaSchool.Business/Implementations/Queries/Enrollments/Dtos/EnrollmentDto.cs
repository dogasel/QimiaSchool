using QimiaSchool.Business.Implementations.Queries.Courses.Dtos;
using QimiaSchool.Business.Implementations.Queries.Student.Dtos;
using QimiaSchool.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Queries.Enrollments.Dtos
{
    public class EnrollmentDto
    {
        public int ID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }

        public CourseDto? Course { get; set; }
        public StudentDto? Student { get; set; }
    }
}
