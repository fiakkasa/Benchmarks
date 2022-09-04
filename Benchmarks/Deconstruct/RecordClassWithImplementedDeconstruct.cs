namespace Benchmarks.Deconstruct;

public record class RecordClassWithImplementedDeconstruct
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
