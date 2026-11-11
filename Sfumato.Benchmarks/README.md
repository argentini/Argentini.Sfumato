# Sfumato Benchmarks

Run benchmarks using Release builds only.

The benchmark project targets .NET 11. Its async benchmarks and the .NET 11 library build use runtime async through `Directory.Build.targets`.

BenchmarkDotNet 0.16.0-preview.2 provides .NET 11 runtime support. The previous stable version, 0.15.8, cannot run its default toolchain on .NET 11.

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
