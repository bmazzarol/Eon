# Minute of Hour

@Eon.Schedule.MinuteOfHour* is a Cron-like @Eon.Schedule that recurs every
specified `minute` of each hour

> [!NOTE]
> `minute` must be between 1 and 59. Any other value will be round down or up

[!code-csharp[Example1](../../../Eon.Tests/Examples/MinuteOfHourTests.cs#Example1)]
