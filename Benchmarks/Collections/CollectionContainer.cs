namespace Benchmarks.Collections;

public record CollectionContainer<T>(IEnumerable<T> Items)
{
    public override string ToString() => Items.Count() + " item(s)";
}
