namespace Benchmarks.Strings;

public readonly struct PersonReadonlyStruct : IPerson
{
    public int Age { get; }
    public string FirstName { get; }
    public string LastName { get; }

    public PersonReadonlyStruct(int age, string firstName, string lastName)
    {
        Age = age;
        FirstName = firstName;
        LastName = lastName;
    }
}
