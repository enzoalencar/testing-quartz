using Microsoft.AspNetCore.Mvc;
using QuartzDocs.Jobs;

namespace QuartzDocs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScheduleController(IQuartzJobScheduler quartzJobScheduler) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync(CancellationToken cancellationToken)
    {
        await quartzJobScheduler.ScheduleJobAsync<EmailReminderJob>(TimeSpan.FromSeconds(10), "123", "Hello World", cancellationToken);
        return Ok("Job is scheduled");
    }
}