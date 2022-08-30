using BenchmarkDotNet.Running;
using Benchmarks;

// https://github.com/dotnet/BenchmarkDotNet
BenchmarkRunner.Run<TaskBenchmarks>();
