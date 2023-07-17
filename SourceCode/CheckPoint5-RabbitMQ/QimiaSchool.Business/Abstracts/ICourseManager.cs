using QimiaSchool.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Abstracts
{
    public interface ICourseManager
    {
        public Task CreateCourseAsync(
     Course course,
     CancellationToken cancellationToken);

        public Task<Course> GetCourseByIdAsync(
            int courseId,
            CancellationToken cancellationToken);
        public Task DeleteCourseAsync(
             int courseId,
             CancellationToken cancellationToken);
        public Task UpdateCourseAsync(
             int courseId,
             Course course,
             CancellationToken cancellationToken);
        public Task<List<Course>> GetCoursesAsync(
        CancellationToken cancellationToken);
    }
    
}
