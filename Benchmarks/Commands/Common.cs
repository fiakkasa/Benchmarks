using System.Reflection;
using static Benchmarks.Consts;

namespace Benchmarks.Commands;

internal static class Common
{
    public static Type[] AvailableBenchmarks() =>
        [
            .. Assembly
                .GetExecutingAssembly()
                .DefinedTypes
                .Where(x => !x.IsInterface && !x.IsAbstract && typeof(IBenchmark).IsAssignableFrom(x))
                .OrderBy(x => x.Name)
        ];

    public static string BenchmarksToText(Type[] collection)
    {
        var availableBenchmarks = new StringBuilder();

        foreach (var item in collection.Select(x => x.Name[..x.Name.LastIndexOf(BenchmarkClassPostfix)]))
        {
            availableBenchmarks.AppendLine(item);
        }

        return availableBenchmarks.ToString();
    }
}
