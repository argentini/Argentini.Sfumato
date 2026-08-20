# Sfumato Benchmarks

Run benchmarks using Release builds only.

List available benchmarks:

```shell
dotnet run --project Sfumato.Benchmarks -c Release -- --list flat
```

Validate one benchmark without measuring:

```shell
dotnet run --project Sfumato.Benchmarks -c Release -- --filter "*CssClassCreationBenchmarks.Basic*" --job Dry
```

Run quick measurements during development:

```shell
dotnet run --project Sfumato.Benchmarks -c Release -- --filter "*CssClassCreationBenchmarks*" --job Short --noOverwrite
```

Run affected benchmarks before and after each optimization. Keep both timestamped reports for comparison. Add permanent benchmarks whenever production hot paths change.
