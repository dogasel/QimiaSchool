using QimiaSchool.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Events.Enrollments
{
    public class EnrollmentGetByIdEvent
    {
        public int EnrollmentId { get; set; }
    }
}