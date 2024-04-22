namespace Benchmarks.InOutRef;

public readonly struct PersonReadonlyStruct(int age, string firstName, string lastName) : IPerson
{
    public int Age { get; } = age;
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;
}
