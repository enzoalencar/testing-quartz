using Quartz;

namespace QuartzDocs.Jobs;

public class EmailReminderJob(ILogger<EmailReminderJob> logger) : IJob
{
    public const string Name = nameof(EmailReminderJob);
    
    public async Task Execute(IJobExecutionContext context)
    {
        var data = context.MergedJobDataMap;
        
        string? userId = data.GetString("userId");
        string? message = data.GetString("message");

        try
        {
            if (userId == null) {
                logger.LogError("User ID is required");
                return;
            }

            await Task.Delay(5000);

            logger.LogInformation("Sent reminder to user {UserId}: {Message}", userId, message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send reminder to user {UserId}", userId);
            throw;
        }
    }

}