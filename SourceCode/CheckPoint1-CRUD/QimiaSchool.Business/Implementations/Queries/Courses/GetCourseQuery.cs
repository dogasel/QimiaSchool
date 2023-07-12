using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QimiaSchool.Business.Implementations.Queries.Courses.Dtos;

namespace QimiaSchool.Business.Implementations.Queries.Courses
{
    public class GetCourseQuery : IRequest <CourseDto> 
    {
        public GetCourseQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
