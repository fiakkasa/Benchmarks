namespace Benchmarks.Strings;

public record struct PersonRecordStructWithMutableProperties : IPerson
{
    public int Age { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
