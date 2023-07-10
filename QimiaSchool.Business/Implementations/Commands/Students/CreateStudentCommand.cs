using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QimiaSchool.Business.Implementations.Commands.Students.Dtos;
using QimiaSchool.DataAccess.Entities;
using MediatR;
namespace QimiaSchool.Business.Implementations.Commands.Students
{
    public class CreateStudentCommand : IRequest<int>
    {
        public CreateStudentDto Student { get; set; }
        public CreateStudentCommand(CreateStudentDto student) { Student = student; }
    }
}
