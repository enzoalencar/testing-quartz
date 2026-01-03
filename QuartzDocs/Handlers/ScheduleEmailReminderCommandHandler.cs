using MediatR;
using QuartzDocs.Commands;
using QuartzDocs.Notifications;

namespace QuartzDocs.Handlers;
public class ScheduleEmailReminderCommandHandler(IMediator mediator, ILogger<ScheduleEmailReminderCommandHandler> logger) 
: IRequestHandler<ScheduleEmailReminderCommand, Unit>
{
    public async Task<Unit> Handle(ScheduleEmailReminderCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Processing ScheduleEmailReminderCommand for user {UserId}", request.UserId);
        
        await mediator.Publish(new EmailReminderNotification
        {
            UserId = request.UserId,
            Message = request.Message,
            Delay = request.Delay
        }, cancellationToken);

        return Unit.Value;
    }
}
