using CommandLine;
using static Benchmarks.Consts;

namespace Benchmarks.Commands;

[Verb("info", isDefault: true, aliases: ["i"], HelpText = "Info!")]
public class InfoCommand : ICommandHandler
{
    public async Task Execute(CancellationToken cancellationToken = default) => await Task.Run(() =>
    {
        var collection = Common.AvailableBenchmarks();

        var availableBenchmarks = Common.BenchmarksToText(collection);

        var info =
@$"{WelcomeMessage}
{Separator}

Info
{Separator}
examples:
    Running all benchmarks: dotnet run -c Release -- benchmarks
    Running all benchmarks (shorthand): dotnet run -c Release -- b
    Running all benchmarks: dotnet run -c Release -- benchmarks --selection {AllBenchmarks}
    Running all benchmarks (shorthand): dotnet run -c Release -- b -s {AllBenchmarks}
    Running specific benchmarks: dotnet run -c Release -- benchmarks --selection {string.Join(',', collection.Take(3).Select(x => x.Name[..x.Name.LastIndexOf(BenchmarkClassPostfix)]))}
    Running specific benchmarks (shorthand): dotnet run -c Release -- b -s {string.Join(',', collection.Take(3).Select(x => x.Name[..x.Name.LastIndexOf(BenchmarkClassPostfix)]))}

Available Benchmarks
{Separator}
{availableBenchmarks}";

        Console.WriteLine(info);
    }, cancellationToken);
}
