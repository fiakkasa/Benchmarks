using System.Linq;
namespace Benchmarks.Collections;

[MemoryDiagnoser(false)]
[ThreadingDiagnoser]
[RankColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[BenchmarkCategory(new[] { "Collections", "Sorting", "Concurrency", "Parallelism" })]
public class SortingIntegersBenchmarks : IBenchmark
{
    public static IEnumerable<CollectionContainer<int>> GetIntCollection()
    {
        yield return new(Enumerable.Empty<int>());

        yield return new(Enumerable
            .Range(0, 10)
            .Select(_ => Random.Shared.Next()));

        yield return new(Enumerable
            .Range(0, 1000)
            .Select(_ => Random.Shared.Next()));
    }

    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(GetIntCollection))]
    public void Array_LINQ_OrderBy(CollectionContainer<int> collection)
    {
        var x = 0;
        var col = collection.Items.ToArray().OrderBy(x => x);
        foreach (var item in col)
        {
            x = item;
        }
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetIntCollection))]
    public void List_LINQ_OrderBy(CollectionContainer<int> collection)
    {
        var x = 0;
        var col = collection.Items.ToList().OrderBy(x => x);
        foreach (var item in col)
        {
            x = item;
        }
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetIntCollection))]
    public void Array_LINQ_OrderByDescending(CollectionContainer<int> collection)
    {
        var x = 0;
        var col = collection.Items.ToArray().OrderByDescending(x => x);
        foreach (var item in col)
        {
            x = item;
        }
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetIntCollection))]
    public void List_LINQ_OrderByDescending(CollectionContainer<int> collection)
    {
        var x = 0;
        var col = collection.Items.ToList().OrderByDescending(x => x);
        foreach (var item in col)
        {
            x = item;
        }
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetIntCollection))]
    public void Array_Array_Sort(CollectionContainer<int> collection)
    {
        var x = 0;
        var col = collection.Items.ToArray();
        Array.Sort(col);
        foreach (var item in col)
        {
            x = item;
        }
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetIntCollection))]
    public void List_Sort(CollectionContainer<int> collection)
    {
        var x = 0;
        var col = collection.Items.ToList();
        col.Sort();
        foreach (var item in col)
        {
            x = item;
        }
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetIntCollection))]
    public void Span_FromArray_Sort(CollectionContainer<int> collection)
    {
        var x = 0;
        var col = collection.Items.ToArray().AsSpan();
        col.Sort();
        foreach (var item in col)
        {
            x = item;
        }
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetIntCollection))]
    public void Array_Array_Sort_and_Reverse_Descending(CollectionContainer<int> collection)
    {
        var x = 0;
        var col = collection.Items.ToArray();
        Array.Sort(col);
        Array.Reverse(col);
        foreach (var item in col)
        {
            x = item;
        }
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetIntCollection))]
    public void List_Sort_and_Reverse_Descending(CollectionContainer<int> collection)
    {
        var x = 0;
        var col = collection.Items.ToList();
        col.Sort();
        col.Reverse();
        foreach (var item in col)
        {
            x = item;
        }
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetIntCollection))]
    public void Array_Array_Sort_with_Comparison_and_CompareTo_Descending(CollectionContainer<int> collection)
    {
        var x = 0;
        var col = collection.Items.ToArray();
        Array.Sort(col, new Comparison<int>((a, b) => b.CompareTo(a)));
        foreach (var item in col)
        {
            x = item;
        }
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetIntCollection))]
    public void Array_Array_Sort_with_Comparison_and_Subtraction_Descending(CollectionContainer<int> collection)
    {
        var x = 0;
        var col = collection.Items.ToArray();
        Array.Sort(col, new Comparison<int>(new Comparison<int>((a, b) => b - a)));
        foreach (var item in col)
        {
            x = item;
        }
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetIntCollection))]
    public void Array_Array_Sort_with_Lamda_and_CompareTo_Descending(CollectionContainer<int> collection)
    {
        var x = 0;
        var col = collection.Items.ToArray();
        Array.Sort(col, (a, b) => b.CompareTo(a));
        foreach (var item in col)
        {
            x = item;
        }
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetIntCollection))]
    public void Array_Array_Sort_with_Lamda_and_Subtraction_Descending(CollectionContainer<int> collection)
    {
        var x = 0;
        var col = collection.Items.ToArray();
        Array.Sort(col, (a, b) => b - a);
        foreach (var item in col)
        {
            x = item;
        }
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetIntCollection))]
    public void List_Sort_with_Comparison_and_CompareTo_Descending(CollectionContainer<int> collection)
    {
        var x = 0;
        var col = collection.Items.ToList();
        col.Sort(new Comparison<int>((a, b) => b.CompareTo(a)));
        foreach (var item in col)
        {
            x = item;
        }
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetIntCollection))]
    public void List_Sort_with_Comparison_and_Subtraction_Descending(CollectionContainer<int> collection)
    {
        var x = 0;
        var col = collection.Items.ToList();
        col.Sort(new Comparison<int>(new Comparison<int>((a, b) => b - a)));
        foreach (var item in col)
        {
            x = item;
        }
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetIntCollection))]
    public void List_Sort_with_Lamda_and_CompareTo_Descending(CollectionContainer<int> collection)
    {
        var x = 0;
        var col = collection.Items.ToList();
        col.Sort((a, b) => b.CompareTo(a));
        foreach (var item in col)
        {
            x = item;
        }
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetIntCollection))]
    public void List_Sort_with_Lamda_and_Subtraction_Descending(CollectionContainer<int> collection)
    {
        var x = 0;
        var col = collection.Items.ToList();
        col.Sort((a, b) => b - a);
        foreach (var item in col)
        {
            x = item;
        }
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetIntCollection))]
    public void Span_FromArray_Array_Sort_with_Comparison_and_CompareTo_Descending(CollectionContainer<int> collection)
    {
        var x = 0;
        var col = collection.Items.ToArray().AsSpan();
        col.Sort(new Comparison<int>((a, b) => b.CompareTo(a)));
        foreach (var item in col)
        {
            x = item;
        }
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetIntCollection))]
    public void Span_FromArray_Array_Sort_with_Comparison_and_Subtraction_Descending(CollectionContainer<int> collection)
    {
        var x = 0;
        var col = collection.Items.ToArray().AsSpan();
        col.Sort(new Comparison<int>(new Comparison<int>((a, b) => b - a)));
        foreach (var item in col)
        {
            x = item;
        }
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetIntCollection))]
    public void Span_FromArray_Array_Sort_with_Lamda_and_CompareTo_Descending(CollectionContainer<int> collection)
    {
        var x = 0;
        var col = collection.Items.ToArray().AsSpan();
        col.Sort((a, b) => b.CompareTo(a));
        foreach (var item in col)
        {
            x = item;
        }
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetIntCollection))]
    public void Span_FromArray_Array_Sort_with_Lamda_and_Subtraction_Descending(CollectionContainer<int> collection)
    {
        var x = 0;
        var col = collection.Items.ToArray().AsSpan();
        col.Sort((a, b) => b - a);
        foreach (var item in col)
        {
            x = item;
        }
    }
}