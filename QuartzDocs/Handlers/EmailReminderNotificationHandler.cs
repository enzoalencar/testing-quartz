using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using QuartzDocs.Jobs;
using QuartzDocs.Notifications;

namespace QuartzDocs.Handlers;
public class EmailReminderNotificationHandler(IQuartzJobScheduler quartzJobScheduler, ILogger<EmailReminderNotificationHandler> logger) 
: INotificationHandler<EmailReminderNotification>
{
    public async Task Handle(EmailReminderNotification notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling EmailReminderNotification for user {UserId}", notification.UserId);
        await quartzJobScheduler.ScheduleJobAsync<EmailReminderJob>(notification.Delay, notification.UserId, notification.Message, cancellationToken);
    }
}
