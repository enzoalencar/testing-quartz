using Microsoft.AspNetCore.Mvc;
using QuartzDocs.Jobs;
using MediatR;
using QuartzDocs.Commands;

namespace QuartzDocs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScheduleController(IQuartzJobScheduler quartzJobScheduler, IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync(CancellationToken cancellationToken)
    {
        await quartzJobScheduler.ScheduleJobAsync<EmailReminderJob>(TimeSpan.FromSeconds(10), "123", "Hello World", cancellationToken);
        return Ok("Job is scheduled");
    }
    
    [HttpPost("mediator")]
    public async Task<IActionResult> PostMediatorAsync(CancellationToken cancellationToken)
    {
        var command = new ScheduleEmailReminderCommand
		{
			UserId = "123",
            Message = "Hello World",
            Delay = TimeSpan.FromSeconds(10)
		};

        await mediator.Send(command, cancellationToken);
        return Ok("Job is scheduled via MediatR");
    }
}