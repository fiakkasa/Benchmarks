namespace Benchmarks.InOutRef;

[MemoryDiagnoser(false)]
[RankColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[BenchmarkCategory(new[] { "Ref", "Objects", "Structs", "Records" })]
public class RefBenchmarks : IBenchmark
{
    private static void RefFn<T>(ref T value, Func<T> fn)
    {
        value = fn();
    }

    [Benchmark(Baseline = true)]
    public void Int_Ref()
    {
        int value = default;
        RefFn(ref value, () => 100);
    }

    [Benchmark]
    public void PersonClass_Ref()
    {
        PersonClass value = default!;
        RefFn(
            ref value,
            () => new PersonClass
            {
                Age = 33,
                FirstName = "John",
                LastName = "Doe"
            }
        );
    }

    [Benchmark]
    public void PersonStruct_Ref()
    {
        PersonStruct value = default!;
        RefFn(
            ref value,
            () => new PersonStruct
            {
                Age = 33,
                FirstName = "John",
                LastName = "Doe"
            }
        );
    }

    [Benchmark]
    public void PersonReadonlyStruct_Ref()
    {
        PersonReadonlyStruct value = default!;
        RefFn(
            ref value,
            () => new PersonReadonlyStruct(33, "John", "Doe")
        );
    }

    [Benchmark]
    public void PersonPropertiesReadonlyStruct_Ref()
    {
        PersonPropertiesReadonlyStruct value = default!;
        RefFn(
            ref value,
            () => new PersonPropertiesReadonlyStruct(33, "John", "Doe")
        );
    }

    [Benchmark]
    public void PersonRecordClass_Ref()
    {
        PersonRecordClass value = default!;
        RefFn(
            ref value,
            () => new PersonRecordClass(33, "John", "Doe")
        );
    }

    [Benchmark]
    public void PersonRecordStruct_Ref()
    {
        PersonRecordStruct value = default!;
        RefFn(
            ref value,
            () => new PersonRecordStruct(33, "John", "Doe")
        );
    }

    [Benchmark]
    public void PersonRecordClassWithInitProperties_Ref()
    {
        PersonRecordClassWithInitProperties value = default!;
        RefFn(
            ref value,
            () => new PersonRecordClassWithInitProperties
            {
                Age = 33,
                FirstName = "John",
                LastName = "Doe"
            }
        );
    }

    [Benchmark]
    public void PersonRecordStructWithInitProperties_Ref()
    {
        PersonRecordStructWithInitProperties value = default!;
        RefFn(
            ref value,
            () => new PersonRecordStructWithInitProperties
            {
                Age = 33,
                FirstName = "John",
                LastName = "Doe"
            }
        );
    }

    [Benchmark]
    public void PersonRecordClassWithMutableProperties_Ref()
    {
        PersonRecordClassWithMutableProperties value = default!;
        RefFn(
            ref value,
            () => new PersonRecordClassWithMutableProperties
            {
                Age = 33,
                FirstName = "John",
                LastName = "Doe"
            }
        );
    }

    [Benchmark]
    public void PersonRecordStructWithMutableProperties_Ref()
    {
        PersonRecordStructWithMutableProperties value = default!;
        RefFn(
            ref value,
            () => new PersonRecordStructWithMutableProperties
            {
                Age = 33,
                FirstName = "John",
                LastName = "Doe"
            }
        );
    }
}
