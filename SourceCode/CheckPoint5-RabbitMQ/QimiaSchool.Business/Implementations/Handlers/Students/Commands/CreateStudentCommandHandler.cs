﻿using MediatR;
using QimiaSchool.Business.Abstracts;
using QimiaSchool.Business.Implementations.Commands.Students;
using QimiaSchool.Business.Implementations.Events.Courses;
using QimiaSchool.Business.Implementations.Events.Students;
using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.MessageBroker.Abstractions;

namespace QimiaSchool.Business.Implementations.Handlers.Students.Commands;
public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, int>
{
    private readonly IStudentManager _studentManager;
    private readonly IEventBus _eventBus;

    public CreateStudentCommandHandler(IStudentManager studentManager, IEventBus eventBus)
    {
        _studentManager = studentManager;
        _eventBus = eventBus;

    }

    public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = new Student
        {
            FirstMidName = request.Student.FirstMidName,
            LastName = request.Student.LastName!,
            EnrollmentDate = DateTime.Now,
        };

        await _studentManager.CreateStudentAsync(student, cancellationToken);
        await _eventBus.PublishAsync(new StudentCreatedEvent
        {
            FirstMidName = student.FirstMidName,
            ID = student.ID,
            LastName = student.LastName,

        });

        return student.ID;
    }
}
