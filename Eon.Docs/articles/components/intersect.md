# Intersect (&)

@Eon.Schedule.Intersect* returns the intersect of two @Eon.Schedule.
As long as both of the two @Eon.Schedule are still running it will return the
maximum @Eon.Duration.

Using the @Eon.Schedule.Intersect* method,

[!code-csharp[Example1](../../../Eon.Tests/Examples/IntersectTests.cs#Example1)]

Using the @Eon.Schedule.op_BitwiseAnd*,

[!code-csharp[Example2](../../../Eon.Tests/Examples/IntersectTests.cs#Example2)]

The returned @Eon.Schedule will run as long as the shortest of the two
@Eon.Schedule

[!code-csharp[Example3](../../../Eon.Tests/Examples/IntersectTests.cs#Example3)]

@Eon.Schedule.op_BitwiseAnd* can be used with @Eon.ScheduleTransformer,

[!code-csharp[Example4](../../../Eon.Tests/Examples/IntersectTests.cs#Example4)]
