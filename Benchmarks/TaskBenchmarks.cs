using BenchmarkDotNet.Attributes;
using System.Collections.Concurrent;

namespace Benchmarks;

[MemoryDiagnoser(false)]
[BenchmarkCategory(new[] { "Tasks", "Parallelism", "Collections" })]
public class TaskBenchmarks
{
    private static IEnumerable<Task<long>> Tasks => Enumerable.Range(0, 50).Select(_ =>
        Task.Run(async () =>
        {
            var x = new Random().Next();
            var y = Random.Shared.Next();

            var z = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            await Task.Delay(50);

            return (x * y) + z;
        })
    );

    [Benchmark]
    public async Task Foreach_Loop()
    {
        foreach (var item in Tasks)
        {
            await item;
        }
    }

    [Benchmark]
    public async Task Async_Foreach_Loop()
    {
        await foreach (var item in Tasks.ToAsyncEnumerable())
        {
            await item;
        }
    }

    [Benchmark]
    public async Task AsyncEnumerable()
    {
        await Tasks.ToAsyncEnumerable()
            .SelectAwait(async x => await x)
            .ToListAsync();
    }

    [Benchmark]
    public void ParallelForEach()
    {
        Parallel.ForEach(Tasks, t => t.GetAwaiter().GetResult());
    }

    [Benchmark]
    public async Task ParallelForEachAsync()
    {
        await Parallel.ForEachAsync(Tasks, async (t, _) => await t);
    }

    [Benchmark]
    public async Task WhenAll()
    {
        await Task.WhenAll(Tasks);
    }

    [Benchmark]
    public async Task WhenAll_with_Chunk_of_4_and_Foreach_Loop()
    {
        foreach (var partition in Tasks.Chunk(4))
        {
            await Task.WhenAll(partition);
        }
    }

    [Benchmark]
    public async Task WhenAll_with_GroupBy_Grouped_by_4_and_Foreach_Loop()
    {
        foreach (var partition in Tasks.Select((t, i) => (t, i)).GroupBy(x => x.i % 4))
        {
            await Task.WhenAll(partition.Select(x => x.t));
        }
    }

    [Benchmark]
    public async Task WhenAll_with_AsParallel_and_DegreeOfParallelism_4()
    {
        await Task.WhenAll(
            Tasks
                .AsParallel()
                .WithDegreeOfParallelism(4)
        );
    }

    private static async Task<List<long>> EvaluatePartition(IEnumerator<Task<long>> partition)
    {
        var result = new List<long>();

        using (partition)
        {
            while (partition.MoveNext())
            {
                result.Add(await partition.Current);
            }
        }

        return result;
    }

    [Benchmark]
    public async Task WhenAll_with_Partitioner_and_Partitions_4()
    {
        await Task.WhenAll(
            Partitioner
                .Create(Tasks)
                .GetPartitions(4)
                .Select(EvaluatePartition)
        );
    }

    [Benchmark]
    public async Task WhenAll_with_Partitioner_AsParallel_and_Partitions_4()
    {
        await Task.WhenAll(
            Partitioner
                .Create(Tasks)
                .GetPartitions(4)
                .AsParallel()
                .Select(EvaluatePartition)
        );
    }

    [Benchmark]
    public async Task AsyncEnumerable_with_GroupBy_Grouped_by_4_and_TaskWhenAll()
    {
        await Tasks.ToAsyncEnumerable()
            .Select((t, i) => (t, i))
            .GroupBy(x => x.i % 4)
            .SelectAwait(async x => await Task.WhenAll(await x.Select(y => y.t).ToListAsync()))
            .ToListAsync();
    }
}
