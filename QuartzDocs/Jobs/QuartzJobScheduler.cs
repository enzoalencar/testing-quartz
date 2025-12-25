using Quartz;

namespace QuartzDocs.Jobs;

public class QuartzJobScheduler(ISchedulerFactory schedulerFactory, ILogger<QuartzJobScheduler> logger) : IQuartzJobScheduler
{
    private IScheduler? _scheduler;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _scheduler = await schedulerFactory.GetScheduler(cancellationToken);
        await _scheduler.Start(cancellationToken);
    }

    public async Task ScheduleJobAsync<TJob>(TimeSpan delay, string userId, string message, CancellationToken cancellationToken) where TJob : IJob
    {
        logger.LogInformation("Scheduling job {JobName} for user {UserId} with message {Message}", typeof(TJob).Name, userId, message);

        var jobDetail = JobBuilder.Create<TJob>()
            .WithIdentity($"{typeof(TJob).Name}-{userId}")
            .UsingJobData("userId", userId)
            .UsingJobData("message", message)
            .Build();

        var scheduler = await schedulerFactory.GetScheduler();
        var trigger = TriggerBuilder.Create()
            .StartAt(DateTimeOffset.Now.Add(delay))
            .Build();

        await scheduler.ScheduleJob(jobDetail, trigger, cancellationToken);
    }
}