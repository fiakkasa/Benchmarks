namespace Benchmarks.InOutRef;

public struct PersonPropertiesReadonlyStruct : IPerson
{
    public int Age { get; }
    public string FirstName { get; }
    public string LastName { get; }

    public PersonPropertiesReadonlyStruct(int age, string firstName, string lastName)
    {
        Age = age;
        FirstName = firstName;
        LastName = lastName;
    }
}
