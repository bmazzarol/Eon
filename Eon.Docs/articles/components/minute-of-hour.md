# Minute of Hour

<xref:Eon.Schedule.MinuteOfHour*> is a Cron-like <xref:Eon.Schedule> that
recurs every specified `minute` of each hour

> [!NOTE]
> `minute` must be between 0 and 59. Any other value will be round down or up

[!code-csharp[Example1](../../../Eon.Tests/Examples/MinuteOfHourTests.cs#Example1)]

[!INCLUDE[](../../../Eon.Tests/Examples/__examples__/MinuteOfHourTests.Case1.md)]
