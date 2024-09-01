using Polly;
using Polly.Retry;

namespace Eon.Tests.Examples;

public static class PollyExamples
{
    [Fact(DisplayName = "Schedule can be used with polly")]
    public static async Task Case1()
    {
        #region Example1

        // build a schedule using Eon
        Schedule schedule = Schedule.Linear(1).Take(5);
        // now create the options
        RetryStrategyOptions options = new RetryStrategyOptions
        {
            MaxRetryAttempts = schedule.Count ?? int.MaxValue,
            Delay = TimeSpan.Zero,
            DelayGenerator = x =>
                ValueTask.FromResult<TimeSpan?>((TimeSpan)schedule[x.AttemptNumber]),
        };
        ResiliencePipeline pipeline = new ResiliencePipelineBuilder().AddRetry(options).Build();
        int attempts = 0;
        await pipeline.ExecuteAsync(async token =>
        {
            await Task.Yield();
            attempts++;
            if (attempts < 5)
            {
                throw new Exception();
            }
        });
        attempts.Should().Be(5);

        #endregion
    }

    [Fact(DisplayName = "Infinite Schedule can be used with polly")]
    public static async Task Case2()
    {
        #region Example2

        // build a schedule using Eon
        Schedule schedule = Schedule.Linear(1);
        // now create the options
        RetryStrategyOptions options = new RetryStrategyOptions
        {
            MaxRetryAttempts = int.MaxValue,
            Delay = TimeSpan.Zero,
            DelayGenerator = x =>
                ValueTask.FromResult<TimeSpan?>((TimeSpan)schedule[x.AttemptNumber]),
        };
        ResiliencePipeline pipeline = new ResiliencePipelineBuilder().AddRetry(options).Build();
        int attempts = 0;
        await pipeline.ExecuteAsync(async token =>
        {
            await Task.Yield();
            attempts++;
            if (attempts < 10)
            {
                throw new Exception();
            }
        });
        attempts.Should().Be(10);

        #endregion
    }
}
