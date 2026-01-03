using MediatR;

namespace QuartzDocs.Commands;
public class ScheduleEmailReminderCommand : IRequest<Unit>
{
    public string UserId { get; set; }
    public string Message { get; set; }
    public TimeSpan Delay { get; set; }
}
