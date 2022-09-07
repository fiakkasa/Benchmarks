namespace Benchmarks.InOutRef;

public record struct PersonRecordClassWithMutableProperties : IPerson
{
    public int Age { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
