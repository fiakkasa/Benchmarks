namespace Benchmarks.InOutRef;

public class PersonClass : IPerson
{
    public int Age { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
