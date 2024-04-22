using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Mathematics;
using BenchmarkDotNet.Running;
using CommandLine;
using static Benchmarks.Consts;

namespace Benchmarks.Commands;

[Verb("benchmarks", aliases: ["b"], HelpText = "Benchmarks!")]
public class BenchmarksCommand : ICommandHandler
{
    [Option(
        's',
        "selection",
        HelpText = $"Defaults to '{AllBenchmarks}'; alternatively a comma separated list can be provide with a selection of benchmarks to run."
    )]
    public string Selection { get; set; } = AllBenchmarks;

    [Option(
        "launchCount",
        HelpText = $"Number of times a process will be launched per benchmark; min: {MinBenchmarkCountString}, max: {MaxBenchmarkCountString}."
    )]
    public int LaunchCount { get; set; } = DefaultBenchmarkCount;

    [Option(
        "warmupCount",
        HelpText = $"Number of warmup rounds per benchmark; min: {MinBenchmarkCountString}, max: {MaxBenchmarkCountString}."
    )]
    public int WarmupCount { get; set; } = DefaultBenchmarkCount;

    [Option(
        "iterationCount",
        HelpText = $"Number of iterations per benchmark; min: {MinBenchmarkCountString}, max: {MaxBenchmarkCountString}."
    )]
    public int IterationCount { get; set; } = DefaultBenchmarkCount;

    public async Task Execute(CancellationToken cancellationToken = default) => await Task.Run(() =>
    {
        var selectedBenchmarks =
            Selection
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

        if (executableBenchmarks.Length == 0) return;

        var job = new Job("Benchmarks Job");

        job.Run.LaunchCount = NormalizeCount(LaunchCount);
        job.Run.WarmupCount = NormalizeCount(WarmupCount);
        job.Run.IterationCount = NormalizeCount(IterationCount);

        var config =
            ManualConfig
                .Create(DefaultConfig.Instance)
                .AddJob(job)
                .WithOptions(ConfigOptions.StopOnFirstError)
                .AddDiagnoser(new MemoryDiagnoser(new(false)))
                .AddColumn(new RankColumn(NumeralSystem.Arabic))
                .WithOrderer(new DefaultOrderer(SummaryOrderPolicy.FastestToSlowest));

        BenchmarkRunner.Run(executableBenchmarks, config);
    }, cancellationToken);

    private static int NormalizeCount(int value) => value switch
    {
        >= MinBenchmarkCount and <= MaxBenchmarkCount => value,
        _ => DefaultBenchmarkCount
    };
}
