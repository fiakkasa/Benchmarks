using BenchmarkDotNet.Running;
using System.Reflection;
using static Benchmarks.Consts;

var (collection, help) = Assets();

if (args.Any(x => x == ArgumentPrefix + "h" || x == ArgumentLongPrefix + "help"))
    Console.WriteLine(help);
else if (collection.Length == 0)
    PrintHeader(NoBenchmarksRegisteredMessage, help);
else if (args.Any(x => x == ArgumentPrefix + "a" || x == ArgumentLongPrefix + "all"))
    RunBenchmarks(AllBenchmarksMessage, help, collection);
else if (args.Any(x => x.StartsWith(ArgumentPrefix + "b" + ArgumentKeyValueSeparator) || x.StartsWith(ArgumentLongPrefix + "benchmark" + ArgumentKeyValueSeparator)))
{
    var runnable =
        args
            .Where(x => x.StartsWith(ArgumentPrefix + "b" + ArgumentKeyValueSeparator) || x.StartsWith(ArgumentLongPrefix + "benchmark" + ArgumentKeyValueSeparator))
            .Select(x => x.Split(ArgumentKeyValueSeparator).Skip(1).First().Trim() + BenchmarkClassPostfix)
            .Distinct()
            .Join(
                collection,
                b => b,
                t => t.Name,
                (_, t) => t
            )
            .ToArray();

    if (runnable.Length == 0)
    {
        PrintHeader(NoMatchingBenchmarksFoundMessage, help);
        return;
    }

    RunBenchmarks(SpecifiedBenchmarksMessage, help, runnable);
}
else
    RunBenchmarks(AllBenchmarksMessage, help, collection);

static (Type[] collection, string help) Assets()
{
    var collection =
        Assembly
            .GetEntryAssembly()!
            .GetTypes()
            .Where(x => !x.IsInterface && x.GetInterface(nameof(IBenchmark)) is { })
            .OrderBy(x => x.Name)
            .ToArray();
    var availableBenchmarks = new StringBuilder();

    foreach (var item in collection.Select(x => x.Name[..x.Name.LastIndexOf(BenchmarkClassPostfix)]))
    {
        availableBenchmarks.AppendLine(item);
    }

    var help =
@$"{WelcomeMessage}

Arguments
----------
{ArgumentLongPrefix}help or {ArgumentPrefix}h: this help menu
{ArgumentLongPrefix}all or {ArgumentPrefix}a : runs all
{ArgumentLongPrefix}benchmark=<name_of_benchmark> or {ArgumentPrefix}b=<name_of_benchmark>: runs specific benchmark - multiple can be specified

Available Benchmarks
--------------------
{availableBenchmarks}";

    return (collection, help);
}

static void PrintHeader(string title, string help)
{
    Console.WriteLine(help);
    Console.WriteLine(Separator);
    Console.WriteLine(title);
    Console.WriteLine(Separator);
    Console.WriteLine(string.Empty);
}

static void RunBenchmarks(string title, string help, Type[] collection)
{
    PrintHeader(title, help);

    BenchmarkRunner.Run(collection);
}