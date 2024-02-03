# Fixed

<xref:Eon.Schedule.Fixed*> emits a <xref:Eon.Duration> which would run up to the
defined `interval` boundary, enforcing a fixed cadence to the
<xref:Eon.Schedule>.

If the action run between emissions takes longer than the `interval`,
then a <xref:Eon.Duration.Zero> will be emitted, so re-runs will not
"pile up".

``` shell
|-----interval-----|-----interval-----|-----interval-----|
|---------action--------||action|-----|action|-----------|
```

[!code-csharp[Example1](../../../Eon.Tests/Examples/FixedTests.cs#Example1)]
