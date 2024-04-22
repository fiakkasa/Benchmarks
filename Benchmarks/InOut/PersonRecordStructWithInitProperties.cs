namespace Benchmarks.InOutRef;

public readonly record struct PersonRecordStructWithInitProperties : IPerson
{
    public int Age { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
}
