﻿using BenchmarkDotNet.Attributes;
using System.Collections.Concurrent;
using System.Security.Cryptography;

namespace Benchmarks;

[MemoryDiagnoser(false)]
[BenchmarkCategory(new[] { "Tasks", "Concurrency", "Parallelism", "Collections" })]
public class TaskBenchmarks
{
    private const int _chunkSize = 4;
    private const int _collectionSize = 10;
    private const int _numberOfIterations = 100;
    private const int _randomStart = 0;
    private const int _randomEnd = 1_000_000;
    private const int _delay = 50;

    private static IEnumerable<Task<int>> Tasks =>
            Enumerable
                .Range(0, _collectionSize)
                .Select((_, index) => UnitOfWork(index));

    private static IEnumerable<Task<int>> TasksWithDelay =>
        Enumerable
            .Range(0, _collectionSize)
            .Select(async (_, index) =>
            {
                await Task.Delay(_delay);

                return await UnitOfWork(index);
            });

    private static Task<int> UnitOfWork(int index) =>
        Task.Run(() =>
        {
            var result = index;

            for (var i = 0; i < _numberOfIterations; i++)
            {
                var random = RandomNumberGenerator.GetInt32(_randomStart, _randomEnd);

                result =
                    result <= random / 2
                        ? result + i
                        : random;
            }

            return result;
        });

    [Benchmark]
    public async Task Foreach_Loop()
    {
        foreach (var task in Tasks)
        {
            await task;
        }
    }

    [Benchmark]
    public async Task Foreach_Loop_with_Delay()
    {
        foreach (var task in TasksWithDelay)
        {
            await task;
        }
    }

    [Benchmark]
    public async Task Foreach_Loop_AsParallel()
    {
        foreach (var task in Tasks.AsParallel())
        {
            await task;
        }
    }

    [Benchmark]
    public async Task Foreach_Loop_with_Delay_AsParallel()
    {
        foreach (var task in TasksWithDelay.AsParallel())
        {
            await task;
        }
    }

    [Benchmark]
    public async Task Foreach_Loop_AsParallel_and_DegreeOfParallelism_ChunkSize()
    {
        var collection =
            Tasks
                .AsParallel()
                .WithDegreeOfParallelism(_chunkSize);

        foreach (var task in collection)
        {
            await task;
        }
    }

    [Benchmark]
    public async Task Foreach_Loop_with_Delay_AsParallel_and_DegreeOfParallelism_ChunkSize()
    {
        var collection =
            TasksWithDelay
                .AsParallel()
                .WithDegreeOfParallelism(_chunkSize);

        foreach (var task in collection)
        {
            await task;
        }
    }

    [Benchmark]
    public async Task Async_Foreach_Loop()
    {
        await foreach (var task in Tasks.ToAsyncEnumerable())
        {
            await task;
        }
    }

    [Benchmark]
    public async Task Async_Foreach_Loop_with_Delay()
    {
        await foreach (var task in TasksWithDelay.ToAsyncEnumerable())
        {
            await task;
        }
    }

    [Benchmark]
    public async Task AsyncEnumerable()
    {
        await Tasks
            .ToAsyncEnumerable()
            .SelectAwait(async task => await task)
            .ToListAsync();
    }

    [Benchmark]
    public async Task AsyncEnumerable_with_Delay()
    {
        await TasksWithDelay
            .ToAsyncEnumerable()
            .SelectAwait(async task => await task)
            .ToListAsync();
    }

    [Benchmark]
    public void ParallelForEach()
    {
        Parallel.ForEach(Tasks, task => task.GetAwaiter().GetResult());
    }

    [Benchmark]
    public void ParallelForEach_with_Delay()
    {
        Parallel.ForEach(TasksWithDelay, task => task.GetAwaiter().GetResult());
    }

    [Benchmark]
    public async Task ParallelForEachAsync()
    {
        await Parallel.ForEachAsync(Tasks, async (task, _) => await task);
    }

    [Benchmark]
    public async Task ParallelForEachAsync_with_Delay()
    {
        await Parallel.ForEachAsync(TasksWithDelay, async (task, _) => await task);
    }

    [Benchmark]
    public async Task WhenAll()
    {
        await Task.WhenAll(Tasks);
    }

    [Benchmark]
    public async Task WhenAll_with_Delay()
    {
        await Task.WhenAll(TasksWithDelay);
    }

    [Benchmark]
    public async Task WhenAll_with_Chunk_and_Foreach_Loop()
    {
        foreach (var batchOfTasks in Tasks.Chunk(_chunkSize))
        {
            await Task.WhenAll(batchOfTasks);
        }
    }

    [Benchmark]
    public async Task WhenAll_with_Delay_Chunk_and_Foreach_Loop()
    {
        foreach (var batchOfTasks in TasksWithDelay.Chunk(_chunkSize))
        {
            await Task.WhenAll(batchOfTasks);
        }
    }

    [Benchmark]
    public async Task WhenAll_with_GroupBy_Grouped_by_ChunkSize_and_Foreach_Loop()
    {
        var collection =
            Tasks
                .Select((task, index) => (task, index))
                .GroupBy(x => x.index / _chunkSize);

        foreach (var batchOfTasks in collection)
        {
            await Task.WhenAll(batchOfTasks.Select(x => x.task));
        }
    }

    [Benchmark]
    public async Task WhenAll_with_Delay_GroupBy_Grouped_by_ChunkSize_and_Foreach_Loop()
    {
        var collection =
            TasksWithDelay
                .Select((task, index) => (task, index))
                .GroupBy(x => x.index / _chunkSize);

        foreach (var batchOfTasks in collection)
        {
            await Task.WhenAll(batchOfTasks.Select(x => x.task));
        }
    }

    [Benchmark]
    public async Task WhenAll_with_AsParallel_and_DegreeOfParallelism_ChunkSize()
    {
        await Task.WhenAll(
            Tasks
                .AsParallel()
                .WithDegreeOfParallelism(_chunkSize)
        );
    }

    [Benchmark]
    public async Task WhenAll_with_Delay_AsParallel_and_DegreeOfParallelism_ChunkSize()
    {
        await Task.WhenAll(
            TasksWithDelay
                .AsParallel()
                .WithDegreeOfParallelism(_chunkSize)
        );
    }

    private static async Task<List<int>> EvaluatePartition(IEnumerator<Task<int>> partition)
    {
        var result = new List<int>();

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
    public async Task WhenAll_with_Partitioner_and_Partitions_ChunkSize()
    {
        await Task.WhenAll(
            Partitioner
                .Create(Tasks)
                .GetPartitions(_chunkSize)
                .Select(EvaluatePartition)
        );
    }

    [Benchmark]
    public async Task WhenAll_with_Delay_Partitioner_and_Partitions_ChunkSize()
    {
        await Task.WhenAll(
            Partitioner
                .Create(TasksWithDelay)
                .GetPartitions(_chunkSize)
                .Select(EvaluatePartition)
        );
    }

    [Benchmark]
    public async Task WhenAll_with_Partitioner_AsParallel_and_Partitions_ChunkSize()
    {
        await Task.WhenAll(
            Partitioner
                .Create(Tasks)
                .GetPartitions(_chunkSize)
                .Select(EvaluatePartition)
                .AsParallel()
        );
    }

    [Benchmark]
    public async Task WhenAll_with_Delay_Partitioner_AsParallel_and_Partitions_ChunkSize()
    {
        await Task.WhenAll(
            Partitioner
                .Create(TasksWithDelay)
                .GetPartitions(_chunkSize)
                .Select(EvaluatePartition)
                .AsParallel()
        );
    }

    [Benchmark]
    public async Task WhenAll_with_Partitioner_Partitions_ChunkSize_AsParallel_and_DegreeOfParallelism_ChunkSize()
    {
        await Task.WhenAll(
            Partitioner
                .Create(Tasks)
                .GetPartitions(_chunkSize)
                .Select(EvaluatePartition)
                .AsParallel()
                .WithDegreeOfParallelism(_chunkSize)
        );
    }

    [Benchmark]
    public async Task WhenAll_with_Delay_Partitioner_Partitions_ChunkSize_AsParallel_and_DegreeOfParallelism_ChunkSize()
    {
        await Task.WhenAll(
            Partitioner
                .Create(TasksWithDelay)
                .GetPartitions(_chunkSize)
                .Select(EvaluatePartition)
                .AsParallel()
                .WithDegreeOfParallelism(_chunkSize)
        );
    }

    [Benchmark]
    public async Task AsyncEnumerable_with_Chunk_and_TaskWhenAll()
    {
        await Tasks
            .Chunk(_chunkSize)
            .ToAsyncEnumerable()
            .SelectAwait(async x => await Task.WhenAll(x))
            .ToListAsync();
    }

    [Benchmark]
    public async Task AsyncEnumerable_with_Delay_Chunk_and_TaskWhenAll()
    {
        await TasksWithDelay
            .Chunk(_chunkSize)
            .ToAsyncEnumerable()
            .SelectAwait(async x => await Task.WhenAll(x))
            .ToListAsync();
    }

    [Benchmark]
    public async Task AsyncEnumerable_with_GroupBy_Grouped_by_ChunkSize_and_TaskWhenAll()
    {
        await Tasks
            .ToAsyncEnumerable()
            .Select((task, index) => (task, index))
            .GroupBy(x => x.index / _chunkSize)
            .SelectAwait(async x => await Task.WhenAll(await x.Select(y => y.task).ToListAsync()))
            .ToListAsync();
    }

    [Benchmark]
    public async Task AsyncEnumerable_with_Delay_GroupBy_Grouped_by_ChunkSize_and_TaskWhenAll()
    {
        await TasksWithDelay
            .ToAsyncEnumerable()
            .Select((task, index) => (task, index))
            .GroupBy(x => x.index / _chunkSize)
            .SelectAwait(async x => await Task.WhenAll(
                await x
                    .Select(y => y.task)
                    .ToListAsync()
                )
            )
            .ToListAsync();
    }
}
