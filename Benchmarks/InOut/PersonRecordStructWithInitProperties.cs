namespace Benchmarks.InOutRef;

public record struct PersonRecordStructWithInitProperties : IPerson
{
    public int Age { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
}
