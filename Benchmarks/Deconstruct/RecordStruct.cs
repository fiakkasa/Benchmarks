namespace Benchmarks.Deconstruct;

public record struct RecordStruct
{
    public string? Name { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public double Weight { get; set; }
}
