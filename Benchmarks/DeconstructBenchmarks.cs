using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Benchmarks.Interfaces;

namespace Benchmarks.Objects;

public record NativeDeconstruct(string? Name, DateTimeOffset DateOfBirth, double Weight);

public record ImplementedDeconstruct
{
    public string? Name { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public double Weight { get; set; }

    public void Deconstruct(out string? name, out DateTimeOffset dateOfBirth, out double weight)
    {
        name = Name;
        dateOfBirth = DateOfBirth;
        weight = Weight;
    }
}

public record ExtensionDeconstruct
{
    public string? Name { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public double Weight { get; set; }
}

public class ExtensionOfClassDeconstruct
{
    public string? Name { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public double Weight { get; set; }
}

public static class ExtDeconstructExtensions
{
    public static void Deconstruct(this ExtensionDeconstruct obj, out string? name, out DateTimeOffset dateOfBirth, out double weight)
    {
        name = obj.Name;
        dateOfBirth = obj.DateOfBirth;
        weight = obj.Weight;
    }

    public static void Deconstruct(this ExtensionOfClassDeconstruct obj, out string? name, out DateTimeOffset dateOfBirth, out double weight)
    {
        name = obj.Name;
        dateOfBirth = obj.DateOfBirth;
        weight = obj.Weight;
    }
}

[MemoryDiagnoser(false)]
[RankColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[BenchmarkCategory(new[] { "Objects", "Deconstruct" })]
public class DeconstructBenchmarks : IBenchmark
{
    [Benchmark(Baseline = true)]
    public void Native_Deconstruct_Record()
    {
        var (Name, DateOfBirth, Weight) = new NativeDeconstruct(
            Name: "John",
            DateOfBirth: DateTimeOffset.Now,
            Weight: 50D
        );
    }

    [Benchmark]
    public void Implemented_Deconstruct_Record()
    {
        var (Name, DateOfBirth, Weight) = new ImplementedDeconstruct
        {
            Name = "John",
            DateOfBirth = DateTimeOffset.Now,
            Weight = 50D
        };
    }

    [Benchmark]
    public void Extension_Deconstruct_Record()
    {
        var (Name, DateOfBirth, Weight) = new ExtensionDeconstruct
        {
            Name = "John",
            DateOfBirth = DateTimeOffset.Now,
            Weight = 50D
        };
    }

    [Benchmark]
    public void Extension_Deconstruct_Class()
    {
        var (Name, DateOfBirth, Weight) = new ExtensionOfClassDeconstruct
        {
            Name = "John",
            DateOfBirth = DateTimeOffset.Now,
            Weight = 50D
        };
    }

    [Benchmark]
    public void Tuple_Deconstruct()
    {
        var (Name, DateOfBirth, Weight) = (
            "John",
            DateTimeOffset.Now,
            50D
        );
    }
}
