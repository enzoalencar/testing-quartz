using Quartz;

namespace QuartzDocs.Jobs;

public interface IQuartzJobScheduler
{
    Task ScheduleJobAsync<TJob>(TimeSpan delay, string userId, string message, CancellationToken cancellationToken) where TJob : IJob;
}