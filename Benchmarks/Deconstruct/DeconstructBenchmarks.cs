namespace Benchmarks.Deconstruct;

[MemoryDiagnoser(false)]
[RankColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[BenchmarkCategory(new[] { "Deconstruct", "Objects", "Structs", "Records" })]
public class DeconstructBenchmarks : IBenchmark
{
    [Benchmark(Baseline = true)]
    public void Record_Class_Native_Deconstruct()
    {
        var (Name, DateOfBirth, Weight) = new RecordClassTerse(
            Name: "John",
            DateOfBirth: DateTimeOffset.Now,
            Weight: 50D
        );
    }

    [Benchmark]
    public void Record_Struct_Native_Deconstruct()
    {
        var (Name, DateOfBirth, Weight) = new RecordStructTerse(
            Name: "John",
            DateOfBirth: DateTimeOffset.Now,
            Weight: 50D
        );
    }

    [Benchmark]
    public void Record_Class_With_Implemented_Deconstruct()
    {
        var (Name, DateOfBirth, Weight) = new RecordClassWithImplementedDeconstruct
        {
            Name = "John",
            DateOfBirth = DateTimeOffset.Now,
            Weight = 50D
        };
    }

    [Benchmark]
    public void Record_Struct_With_Implemented_Deconstruct()
    {
        var (Name, DateOfBirth, Weight) = new RecordStructWithImplementedDeconstruct
        {
            Name = "John",
            DateOfBirth = DateTimeOffset.Now,
            Weight = 50D
        };
    }

    [Benchmark]
    public void Record_Class_Deconstruct_Extension()
    {
        var (Name, DateOfBirth, Weight) = new RecordClass
        {
            Name = "John",
            DateOfBirth = DateTimeOffset.Now,
            Weight = 50D
        };
    }

    [Benchmark]
    public void Record_Struct_Deconstruct_Extension()
    {
        var (Name, DateOfBirth, Weight) = new RecordStruct
        {
            Name = "John",
            DateOfBirth = DateTimeOffset.Now,
            Weight = 50D
        };
    }

    [Benchmark]
    public void Record_Class_Deconstruct_Extension_As_Method()
    {
        new RecordClass
        {
            Name = "John",
            DateOfBirth = DateTimeOffset.Now,
            Weight = 50D
        }.Deconstruct(out var Name, out var DateOfBirth, out var Weight);
    }

    [Benchmark]
    public void Record_Struct_Deconstruct_Extension_As_Method()
    {
        new RecordStruct
        {
            Name = "John",
            DateOfBirth = DateTimeOffset.Now,
            Weight = 50D
        }.Deconstruct(out var Name, out var DateOfBirth, out var Weight);
    }

    [Benchmark]
    public void Class_Deconstruct_Extension()
    {
        var (Name, DateOfBirth, Weight) = new Class
        {
            Name = "John",
            DateOfBirth = DateTimeOffset.Now,
            Weight = 50D
        };
    }

    [Benchmark]
    public void Struct_Deconstruct_Extension()
    {
        var (Name, DateOfBirth, Weight) = new Struct
        {
            Name = "John",
            DateOfBirth = DateTimeOffset.Now,
            Weight = 50D
        };
    }

    [Benchmark]
    public void Class_Deconstruct_Extension_As_Method()
    {
        new Class
        {
            Name = "John",
            DateOfBirth = DateTimeOffset.Now,
            Weight = 50D
        }.Deconstruct(out var Name, out var DateOfBirth, out var Weight);
    }

    [Benchmark]
    public void Struct_Deconstruct_Extension_As_Method()
    {
        new Struct
        {
            Name = "John",
            DateOfBirth = DateTimeOffset.Now,
            Weight = 50D
        }.Deconstruct(out var Name, out var DateOfBirth, out var Weight);
    }

    [Benchmark]
    public void Tuple_Deconstruct_Declared()
    {
        var (Name, DateOfBirth, Weight) = (
            "John",
            DateTimeOffset.Now,
            50D
        );
    }

    [Benchmark]
    public void Tuple_Deconstruct_Assigned()
    {
        var (Name, DateOfBirth, Weight) = new Tuple<string?, DateTimeOffset, double>(
            "John",
            DateTimeOffset.Now,
            50D
        );
    }
}
