# Hour of Day

<xref:Eon.Schedule.HourOfDay*> is a Cron-like <xref:Eon.Schedule> that
recurs every specified `hour` of each day

> [!NOTE]
> `hour` must be between 0 and 23. Any other value will be round down or up

[!code-csharp[Example1](../../../Eon.Tests/Examples/HourOfDayTests.cs#Example1)]
