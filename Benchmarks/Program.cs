using Benchmarks.Commands;
using CommandLine;

using var cts = new CancellationTokenSource();

Console.CancelKeyPress += (s, e) =>
{
    Console.WriteLine("Stopping execution...");
    cts.Cancel();
    e.Cancel = true;
};

try
{
    await Parser.Default
        .ParseArguments<InfoCommand, BenchmarksCommand>(args)
        .WithParsedAsync<ICommandHandler>(async cm => await cm.Execute(cts.Token))
        .ContinueWith(r => r switch
        {
            { IsFaulted: true, Exception: { } ex } => throw ex.GetBaseException(),
            { IsFaulted: true } => throw new InvalidOperationException("Whoops, something went wrong..."),
            _ => r
        });
}
catch (Exception ex)
{
    Console.WriteLine($"Failed with message: '{ex.Message}'");
    Environment.Exit(1);
}
