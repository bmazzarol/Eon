# Append (+)

@Eon.Schedule.Append* appends the `second` @Eon.Schedule to the `first`
@Eon.Schedule.

> [!WARNING]
> Appending to the end of an infinite @Eon.Schedule will not result in
> the `second` @Eon.Schedule getting run, as the `first` will never complete

Using the @Eon.Schedule.Append* method,

[!code-csharp[Example1](../../../Eon.Tests/Examples/AppendTests.cs#Example1)]

Using the @Eon.Schedule.op_Addition*,

[!code-csharp[Example2](../../../Eon.Tests/Examples/AppendTests.cs#Example2)]
