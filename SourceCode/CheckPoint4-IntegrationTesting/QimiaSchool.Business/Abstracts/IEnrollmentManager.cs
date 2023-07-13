using QimiaSchool.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Abstracts
{
    public interface IEnrollmentManager
    {
        public Task CreateEnrollmentAsync(
       Enrollment enrollment,
       CancellationToken cancellationToken);

        public Task<Enrollment> GetEnrollmentByIdAsync(
            int enrollmentId,
            CancellationToken cancellationToken);
        public Task UpdateEnrollmentAsync(
             int enrollmentId,
             Enrollment enrollment,
             CancellationToken cancellationToken);
        public Task DeleteEnrollmentAsync(
            int enrollmentId,
           
            CancellationToken cancellationToken);
        Task<List<Enrollment>> GetEnrollmentsAsync(
        CancellationToken cancellationToken);
    }
}
