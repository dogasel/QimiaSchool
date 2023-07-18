﻿using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace QimiaSchool.Business.Implementations.Commands.Students;
public class DeleteStudentCommand : IRequest<int>
{
    public int Id { get; set; }

    public DeleteStudentCommand(int id)
    {
        Id = id;
    }
}