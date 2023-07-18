﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QimiaSchool.Business.Implementations.Commands.Enrollments;
using QimiaSchool.Business.Implementations.Commands.Enrollments.Dtos;
using QimiaSchool.Business.Implementations.Queries.Enrollments;
using QimiaSchool.Business.Implementations.Queries.Enrollments.Dtos;
using Serilog;
namespace QimiaSchool.Controllers;

[ApiController]
//[Authorize]
[Route("[controller]")]
public class EnrollmentsController : Controller
{
    private readonly IMediator _mediator;
    private readonly Serilog.ILogger _enrollmentLogger;

    public EnrollmentsController(IMediator mediator)
    {
        _mediator = mediator;
        _enrollmentLogger = Log.ForContext(
            "SourceContext",
            typeof(EnrollmentsController).FullName);
    }

    [HttpPost]
    public async Task<ActionResult> CreateEnrollment(
        [FromBody] CreateEnrollmentDto enrollment,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CreateEnrollmentCommand(enrollment), cancellationToken);

        return CreatedAtAction(
            nameof(GetEnrollment),
            new { Id = response },
            enrollment);
    }

    [HttpGet("{id}")]
    public async Task<EnrollmentDto> GetEnrollment(
    [FromRoute] int id,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetEnrollmentQuery(id), cancellationToken);
        return (EnrollmentDto)result;
    }


    [HttpGet]
    public Task<List<EnrollmentDto>> GetEnrollments(CancellationToken cancellationToken)
    {
        return _mediator.Send(
            new GetEnrollmentsQuery(),
            cancellationToken);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateEnrollment(
        [FromRoute] int id,
        [FromBody] UpdateEnrollmentDto enrollment,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new UpdateEnrollmentCommand(id, enrollment),
            cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEnrollment(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new DeleteEnrollmentCommand(id)
,
            cancellationToken);

        return NoContent();
    }
}