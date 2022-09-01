using BenchmarkDotNet.Running;
using Benchmarks.Interfaces;
using System.Reflection;

const string postFix = "Benchmarks";

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
--benchmark:<name_of_benchmark> or -b:<name_of_benchmark>: runs specific benchmark - multiple can specified

Available Benchmarks
--------------------
{string.Join(
    Environment.NewLine,
    collection.Select(x =>
        x.Name.Substring(0, x.Name.LastIndexOf(postFix))
    )
)}";

static void Run(Type[] runnable, string help, string title)
{
    Console.WriteLine(help);
    Console.WriteLine(string.Empty);
    Console.WriteLine("----------------------------------------------------------------");
    Console.WriteLine(title);
    Console.WriteLine("----------------------------------------------------------------");
    Console.WriteLine(string.Empty);

    BenchmarkRunner.Run(runnable);
}

// https://github.com/dotnet/BenchmarkDotNet
if (args.Any(x => x == "-h" || x == "--help"))
    Console.WriteLine(help);
else if (args.Any(x => x == "-a" || x == "--all"))
    Run(collection, help, "All benchmarks will commence promptly!");
else if (args.Any(x => x.StartsWith("-b:") || x.StartsWith("--benchmark:")))
{
    var runnable =
        args
            .Where(x => x.StartsWith("-b:") || x.StartsWith("--benchmark:"))
            .Select(x => x.Split(":").Skip(1).First().Trim() + postFix)
            .Join(
                collection,
                b => b,
                t => t.Name,
                (_, t) => t
            );

    Run(runnable.ToArray(), help, "The specified benchmark(s) will commence promptly!");
}
else
    Run(collection, help, "All benchmarks will commence promptly!");