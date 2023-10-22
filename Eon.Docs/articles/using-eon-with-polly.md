# How to use with Polly.Net

[Polly](https://github.com/App-vNext/Polly) is a popular resilience
and transient-fault-handling library for .NET.

It exposes
a [RetryStrategyOptions](https://github.com/App-vNext/Polly/blob/main/src/Polly.Core/Retry/RetryStrategyOptions.TResult.cs)
which can be used to configure the retry policy.

Integrating a @Eon.Schedule can be done like so,

[!code-csharp[Example1](../../Eon.Tests/Examples/PollyExamples.cs#Example1)]

Using an infinite @Eon.Schedule is also possible,

[!code-csharp[Example1](../../Eon.Tests/Examples/PollyExamples.cs#Example2)]
