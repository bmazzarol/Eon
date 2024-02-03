# Second of Minute

<xref:Eon.Schedule.SecondOfMinute*> is a Cron-like <xref:Eon.Schedule> that
recurs every specified `second` of each minute

> [!NOTE]
> `second` must be between 0 and 59. Any other value will be round down or up

[!code-csharp[Example1](../../../Eon.Tests/Examples/SecondOfMinuteTests.cs#Example1)]
