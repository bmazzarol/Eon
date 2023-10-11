# Union (|)

@Eon.Schedule.Union* returns the union of two @Eon.Schedule.
As long as any of the two @Eon.Schedule are still running it will return the
minimum @Eon.Duration.

Using the @Eon.Schedule.Union* method,

[!code-csharp[Example1](../../../Eon.Tests/Examples/UnionTests.cs#Example1)]

Using the @Eon.Schedule.op_BitwiseOr*,

[!code-csharp[Example2](../../../Eon.Tests/Examples/UnionTests.cs#Example2)]

The returned @Eon.Schedule will run as long as the longest of the two
@Eon.Schedule,

[!code-csharp[Example3](../../../Eon.Tests/Examples/UnionTests.cs#Example3)]

@Eon.Schedule.op_BitwiseOr* can be used with @Eon.ScheduleTransformer,

[!code-csharp[Example4](../../../Eon.Tests/Examples/UnionTests.cs#Example4)]
