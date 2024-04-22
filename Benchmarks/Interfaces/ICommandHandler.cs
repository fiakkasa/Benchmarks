namespace Benchmarks.Interfaces;

public interface ICommandHandler
{
    Task Execute(CancellationToken cancellationToken = default);
}
