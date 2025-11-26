```

BenchmarkDotNet v0.15.6, Linux Fedora Linux 42 (Workstation Edition)
12th Gen Intel Core i5-12450H 0.40GHz, 1 CPU, 12 logical and 8 physical cores
.NET SDK 10.0.100
  [Host]     : .NET 10.0.0 (10.0.0, 10.0.25.52411), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.0 (10.0.0, 10.0.25.52411), X64 RyuJIT x86-64-v3


```
| Method                          | Mean           | Error       | StdDev      | Ratio      | RatioSD    | Gen0   | Allocated | Alloc Ratio |
|-------------------------------- |---------------:|------------:|------------:|-----------:|-----------:|-------:|----------:|------------:|
| AcessoSingletonPuro             |      0.1374 ns |   0.0490 ns |   0.0458 ns |       1.64 |       2.68 |      - |         - |          NA |
| AcessoOptionsMonitor            |     14.5177 ns |   0.1448 ns |   0.1209 ns |     173.57 |     264.39 |      - |         - |          NA |
| EscritaEmDisco_E_LeituraMonitor | 17,327.2034 ns | 275.6779 ns | 257.8692 ns | 207,159.04 | 315,482.15 | 0.1221 |     866 B |          NA |
