# Benchmarks

| Arguments     | Description                                     |
| ------------- | ----------------------------------------------- |
| info, i       | Info!                                           |
| benchmarks, b | Benchmarks!                                     |
| help          | Display more information on a specific command. |
| version       | Display version information.                    |

Available Benchmarks

- Deconstruct
- In
- Out
- Ref
- SortingIntegers
- StringConcatenation
- TasksCollection
- TasksWithDelayCollection

ex.

Running all benchmarks:

```powershell
dotnet run -c Release -- benchmarks
```

Running specific benchmarks:

```powershell
dotnet run -c Release -- benchmarks --selection Deconstruct,In,Out
```
