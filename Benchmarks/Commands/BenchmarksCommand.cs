using BenchmarkDotNet.Running;
using CommandLine;
using static Benchmarks.Consts;

namespace Benchmarks.Commands;

[Verb("benchmarks", aliases: ["b"], HelpText = "Benchmarks!")]
public class BenchmarksCommand : ICommandHandler
{
    [Option('s', "selection", HelpText = $"Defaults to '{AllBenchmarks}'; alternatively a comma separated list can be provide with a selection of benchmarks to run.")]
    public string Option { get; set; } = AllBenchmarks;

    public async Task Execute()
    {
        var selectedBenchmarks =
            Option
                .Split(',')
                .Select(x => x.Trim())
                .DistinctBy(x => x.ToLower())
                .ToArray();
        var allBenchmarks = Common.AvailableBenchmarks();

        var executableBenchmarks = selectedBenchmarks switch
        {
            { Length: 0 } => [],
            { } col when col.Contains(AllBenchmarks, StringComparer.InvariantCultureIgnoreCase) => allBenchmarks,
            _ => selectedBenchmarks
                .Distinct()
                .Join(
                    allBenchmarks,
                    b => b,
                    t => t.Name[..t.Name.LastIndexOf(BenchmarkClassPostfix)],
                    (_, t) => t
                )
                .ToArray()
        };
        var info = executableBenchmarks switch
        {
            { Length: > 0 } =>
@$"Benchmarks to run
{Separator}
{Common.BenchmarksToText(executableBenchmarks)}",
            _ => NoMatchingBenchmarksFoundMessage
        };

        Console.WriteLine(info);

        if(executableBenchmarks.Length == 0) return;

        BenchmarkRunner.Run(executableBenchmarks);
    }
}
