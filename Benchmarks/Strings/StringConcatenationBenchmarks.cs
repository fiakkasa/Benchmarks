namespace Benchmarks.Strings;

[MemoryDiagnoser(false)]
[RankColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[BenchmarkCategory(new[] { "Strings", "Scalars", "StringConcatenation" })]
public class StringConcatenationBenchmarks : IBenchmark
{
    private readonly string[] _collection = new[]
    {
        "hello",
        "world",
        "lorem",
        "ipsum",
        "apple",
        "strawberry",
        "spaghetti",
        "lasagna",
        "red",
        "blue"
    };

    [Benchmark(Baseline = true)]
    public string Concatenate_Using_Plus()
    {
        return _collection[0]
            + _collection[1]
            + _collection[2]
            + _collection[3]
            + _collection[4]
            + _collection[5]
            + _collection[6]
            + _collection[7]
            + _collection[8]
            + _collection[9];
    }

    [Benchmark]
    public string Concatenate_Using_ForeachLoop_and_Plus()
    {
        var result = string.Empty;

        foreach (var item in _collection)
        {
            result += item;
        }

        return result;
    }

    [Benchmark]
    public string Concatenate_Using_ForeachLoop_and_StringBuilder()
    {
        var result = new StringBuilder();

        foreach (var item in _collection)
        {
            result.Append(result);
        }

        return result.ToString();
    }

    [Benchmark]
    public string Concatenate_Using_String_Join()
    {
        return string.Join(string.Empty, _collection);
    }

    [Benchmark]
    public string Concatenate_Using_String_Concat()
    {
        return string.Concat(_collection);
    }

    [Benchmark]
    public string Concatenate_Using_Aggregate_Plus()
    {
        return _collection.Aggregate(
            string.Empty,
            (acc, item) => acc + item
        );
    }

    [Benchmark]
    public string Concatenate_Using_Aggregate_StringBuilder()
    {
        return _collection.Aggregate(
            new StringBuilder(),
            (acc, item) => acc.Append(item)
        ).ToString();
    }

    [Benchmark]
    public string Concatenate_Using_StringBuilder_AppendJoin()
    {
        return new StringBuilder().AppendJoin(string.Empty, _collection).ToString();
    }

    [Benchmark]
    public string Concatenate_Using_Interpolation()
    {
        return $"{_collection[0]}{_collection[1]}{_collection[2]}{_collection[3]}{_collection[4]}{_collection[5]}{_collection[6]}{_collection[7]}{_collection[8]}{_collection[9]}";
    }

    [Benchmark]
    public string Concatenate_Using_String_Format_and_Spread()
    {
        return string.Format(
            "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}",
            _collection[0],
            _collection[1],
            _collection[2],
            _collection[3],
            _collection[4],
            _collection[5],
            _collection[6],
            _collection[7],
            _collection[8],
            _collection[9]
        );
    }

    [Benchmark]
    public string Concatenate_Using_String_Format_and_Collection()
    {
        return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}", _collection);
    }
}
