# Benchmarks

```powershell
Arguments
----------
--help or -h : this help menu
--all or -a : runs all
--benchmark=<name_of_benchmark> or -b=<name_of_benchmark>: runs specific benchmark - multiple can specified

Available Benchmarks
--------------------
TasksCollection
TasksWithDelayCollection
Deconstruct
```

ex.

```powershell
dotnet run -c Release -- --all
```