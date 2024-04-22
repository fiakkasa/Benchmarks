namespace Benchmarks.InOutRef;

[BenchmarkCategory(["In", "Objects", "Structs", "Records"])]
public class InBenchmarks : IBenchmark
{
    private static string InFn<T>(in T value, Func<T, string> fn) =>
        fn(value);

    private static string IPersonToString<T>(T obj) where T : IPerson =>
        $"{obj.Age}-{obj.FirstName}-{obj.LastName}";

    [Benchmark(Baseline = true)]
    public string Int_In() =>
        InFn(100, (x) => $"{x}-{x}-{x}");

    [Benchmark]
    public string PersonClass_In() =>
        InFn(
            new PersonClass
            {
                Age = 33,
                FirstName = "John",
                LastName = "Doe"
            },
            IPersonToString
        );

    [Benchmark]
    public string PersonStruct_In() =>
        InFn(
            new PersonStruct
            {
                Age = 33,
                FirstName = "John",
                LastName = "Doe"
            },
            IPersonToString
        );

    [Benchmark]
    public string PersonReadonlyStruct_In() =>
        InFn(
            new PersonReadonlyStruct(33, "John", "Doe"),
            IPersonToString
        );

    [Benchmark]
    public string PersonPropertiesReadonlyStruct_In() =>
        InFn(
            new PersonPropertiesReadonlyStruct(33, "John", "Doe"),
            IPersonToString
        );

    [Benchmark]
    public string PersonRecordClass_In() =>
        InFn(
            new PersonRecordClass(33, "John", "Doe"),
            IPersonToString
        );

    [Benchmark]
    public string PersonRecordStruct_In() =>
        InFn(
            new PersonRecordStruct(33, "John", "Doe"),
            IPersonToString
        );

    [Benchmark]
    public string PersonRecordClassWithInitProperties_In() =>
        InFn(
            new PersonRecordClassWithInitProperties
            {
                Age = 33,
                FirstName = "John",
                LastName = "Doe"
            },
            IPersonToString
        );

    [Benchmark]
    public string PersonRecordStructWithInitProperties_In() =>
        InFn(
            new PersonRecordStructWithInitProperties
            {
                Age = 33,
                FirstName = "John",
                LastName = "Doe"
            },
            IPersonToString
        );

    [Benchmark]
    public string PersonRecordClassWithMutableProperties_In() =>
        InFn(
            new PersonRecordClassWithMutableProperties
            {
                Age = 33,
                FirstName = "John",
                LastName = "Doe"
            },
            IPersonToString
        );

    [Benchmark]
    public string PersonRecordStructWithMutableProperties_In() =>
        InFn(
            new PersonRecordStructWithMutableProperties
            {
                Age = 33,
                FirstName = "John",
                LastName = "Doe"
            },
            IPersonToString
        );
}
