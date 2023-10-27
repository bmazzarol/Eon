# Building a Custom @Eon.Schedule

> [!WARNING]
> Before building a custom @Eon.Schedule consider using the existing
> components and build them up via composition

The recipe for building a @Eon.Schedule is simple.

1. Extend from @Eon.Schedule
2. Implement the GetEnumerator method
3. Implement CanCount
4. Implement Count

> [!NOTE]
> For @Eon.Schedule that are infinite CanCount is `false` and Count is `null`

First lets build a simple @Eon.Schedule that returns
only odd natural numbers in seconds to a provided `max`.

[!code-csharp[Example1](../../../Eon.Tests/Examples/CustomOddNumbers.cs#Example1)]

The logic is all within the GetEnumerator and `yield` and `break`
make it trivial to build any @Eon.Schedule.

For example here is
the Polly.NET [DecorrelatedJitterBackoffV2](https://github.com/App-vNext/Polly/blob/main/src/Polly.Core/Retry/RetryHelper.cs#L86-L113)
as a @Eon.Schedule

[!code-csharp[Example1](../../../Eon.Tests/Examples/DecorrelatedJitterBackoffV2.cs#Example1)]
