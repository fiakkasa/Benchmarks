namespace Benchmarks.Deconstruct;

public static class ExtDeconstructExtensions
{
    public static void Deconstruct(this RecordClass obj, out string? name, out DateTimeOffset dateOfBirth, out double weight)
    {
        name = obj.Name;
        dateOfBirth = obj.DateOfBirth;
        weight = obj.Weight;
    }

    public static void Deconstruct(this RecordStruct obj, out string? name, out DateTimeOffset dateOfBirth, out double weight)
    {
        name = obj.Name;
        dateOfBirth = obj.DateOfBirth;
        weight = obj.Weight;
    }

    public static void Deconstruct(this Class obj, out string? name, out DateTimeOffset dateOfBirth, out double weight)
    {
        name = obj.Name;
        dateOfBirth = obj.DateOfBirth;
        weight = obj.Weight;
    }

    public static void Deconstruct(this Struct obj, out string? name, out DateTimeOffset dateOfBirth, out double weight)
    {
        name = obj.Name;
        dateOfBirth = obj.DateOfBirth;
        weight = obj.Weight;
    }
}
