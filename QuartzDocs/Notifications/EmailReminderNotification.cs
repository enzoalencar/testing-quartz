using MediatR;

namespace QuartzDocs.Notifications;
public class EmailReminderNotification : INotification
{
    public string UserId { get; set; }
    public string Message { get; set; }
    public TimeSpan Delay { get; set; } 
}
