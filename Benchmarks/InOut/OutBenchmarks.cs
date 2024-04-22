namespace Benchmarks.InOutRef;

[BenchmarkCategory(["Out", "Objects", "Structs", "Records"])]
public class OutBenchmarks : IBenchmark
{
    private static void OutFn<T>(out T value, Func<T> fn)
    {
        value = fn();
    }

    [Benchmark(Baseline = true)]
    public void Int_Out()
    {
        OutFn(out int value, () => 100);
    }

    [Benchmark]
    public void PersonClass_Out()
    {
        OutFn(
            out PersonClass value,
            () => new PersonClass
            {
                Age = 33,
                FirstName = "John",
                LastName = "Doe"
            }
        );
    }

    [Benchmark]
    public void PersonStruct_Out()
    {
        OutFn(
            out PersonStruct value,
            () => new PersonStruct
            {
                Age = 33,
                FirstName = "John",
                LastName = "Doe"
            }
        );
    }

    [Benchmark]
    public void PersonReadonlyStruct_Out()
    {
        OutFn(
            out PersonReadonlyStruct value,
            () => new PersonReadonlyStruct(33, "John", "Doe")
        );
    }

    [Benchmark]
    public void PersonPropertiesReadonlyStruct_Out()
    {
        OutFn(
            out PersonPropertiesReadonlyStruct value,
            () => new PersonPropertiesReadonlyStruct(33, "John", "Doe")
        );
    }

    [Benchmark]
    public void PersonRecordClass_Out()
    {
        OutFn(
            out PersonRecordClass value,
            () => new PersonRecordClass(33, "John", "Doe")
        );
    }

    [Benchmark]
    public void PersonRecordStruct_Out()
    {
        OutFn(
            out PersonRecordStruct value,
            () => new PersonRecordStruct(33, "John", "Doe")
        );
    }

    [Benchmark]
    public void PersonRecordClassWithInitProperties_Out()
    {
        OutFn(
            out PersonRecordClassWithInitProperties value,
            () => new PersonRecordClassWithInitProperties
            {
                Age = 33,
                FirstName = "John",
                LastName = "Doe"
            }
        );
    }

    [Benchmark]
    public void PersonRecordStructWithInitProperties_Out()
    {
        OutFn(
            out PersonRecordStructWithInitProperties value,
            () => new PersonRecordStructWithInitProperties
            {
                Age = 33,
                FirstName = "John",
                LastName = "Doe"
            }
        );
    }

    [Benchmark]
    public void PersonRecordClassWithMutableProperties_Out()
    {
        OutFn(
            out PersonRecordClassWithMutableProperties value,
            () => new PersonRecordClassWithMutableProperties
            {
                Age = 33,
                FirstName = "John",
                LastName = "Doe"
            }
        );
    }

    [Benchmark]
    public void PersonRecordStructWithMutableProperties_Out()
    {
        OutFn(
            out PersonRecordStructWithMutableProperties value,
            () => new PersonRecordStructWithMutableProperties
            {
                Age = 33,
                FirstName = "John",
                LastName = "Doe"
            }
        );
    }
}
