namespace Benchmarks.Strings;

public record struct PersonRecordClassWithInitProperties : IPerson
{
    public int Age { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
}
