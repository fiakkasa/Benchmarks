using BenchmarkDotNet.Running;
using Benchmarks;
using Benchmarks.Interfaces;
using System.Reflection;

static (Type[] collection, string help) Assets()
{
    var collection =
        Assembly
            .GetEntryAssembly()!
            .GetTypes()
            .Where(x => !x.IsInterface && x.GetInterface(nameof(IBenchmark)) is { })
            .ToArray();

    var help =
@$"Welcome to Benchmarks!

Arguments
----------
--help or -h : this help menu
--all or -a : runs all
--benchmark=<name_of_benchmark> or -b=<name_of_benchmark>: runs specific benchmark - multiple can specified

Available Benchmarks
--------------------
{string.Join(
    Environment.NewLine,
    collection.Select(x =>
        x.Name[..x.Name.LastIndexOf(Consts.BenchmarkClassPostfix)]
    )
)}";

    return (collection, help);
}

static void PrintHeader(string title, string help)
{
    Console.WriteLine(help);
    Console.WriteLine(string.Empty);
    Console.WriteLine("----------------------------------------------------------------");
    Console.WriteLine(title);
    Console.WriteLine("----------------------------------------------------------------");
    Console.WriteLine(string.Empty);
}

static void RunBenchmarks(string title, string help, Type[] collection)
{
    PrintHeader(title, help);

    BenchmarkRunner.Run(collection);
}

var (collection, help) = Assets();

if (args.Any(x => x == "-h" || x == "--help"))
{
    Console.WriteLine(help);
}
else if (collection.Length == 0)
{
    PrintHeader("No benchmark(s) registered", help);
}
else if (args.Any(x => x == "-a" || x == "--all"))
{
    RunBenchmarks(
        "All benchmarks will commence promptly!",
        help,
        collection
    );
}
else if (args.Any(x => x.StartsWith("-b" + Consts.ArgumentKeyValueSeparator) || x.StartsWith("--benchmark" + Consts.ArgumentKeyValueSeparator)))
{
    var runnable =
        args
            .Where(x => x.StartsWith("-b" + Consts.ArgumentKeyValueSeparator) || x.StartsWith("--benchmark" + Consts.ArgumentKeyValueSeparator))
            .Select(x => x.Split(Consts.ArgumentKeyValueSeparator).Skip(1).First().Trim() + Consts.BenchmarkClassPostfix)
            .Join(
                collection,
                b => b,
                t => t.Name,
                (_, t) => t
            )
            .ToArray();

    if (runnable.Length == 0)
    {
        PrintHeader("No matching benchmark(s) found", help);
        return;
    }

    RunBenchmarks(
        "The specified benchmark(s) will commence promptly!",
        help,
        runnable
    );
}
else
{
    RunBenchmarks(
        "All benchmarks will commence promptly!",
        help,
        collection
    );
}