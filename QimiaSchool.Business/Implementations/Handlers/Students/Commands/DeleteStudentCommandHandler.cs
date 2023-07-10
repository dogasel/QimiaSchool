﻿using MediatR;
using QimiaSchool.Business.Abstracts;
using QimiaSchool.Business.Implementations.Commands.Students;
using QimiaSchool.DataAccess.Entities;

namespace QimiaSchool.Business.Implementations.Handlers.Students.Commands;
public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, int>
{
    private readonly IStudentManager _studentManager;

    public DeleteStudentCommandHandler(IStudentManager studentManager)
    {
        _studentManager = studentManager;
    }

    public async Task<int> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {

        await _studentManager.DeleteStudentAsync(request.Id,cancellationToken);


        return request.Id;
    }
}
