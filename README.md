# .NET BenchMark for IOPtions Pattenr
This project reproduce some cenarios to validate the benchmark for different IOptions patterns.

## How to run
Follow the steps below:
```sh
# Clone this repo
git clone https://github.com/felipetomm/dotnet-options-pattern-benchmark.git

# enter in cloned directory
cd dotnet-options-pattern-benchmark

# execute dotnet run
dotnet run -c Release
```

## Cenarios
### [AcessoSingletonPuro](https://github.com/felipetomm/dotnet-options-pattern-benchmark/blob/main/Program.cs#L62)
Represents the use of an section of appsettings directly via IOptions as a singleton instance.

### [AcessoOptionsMonitor](https://github.com/felipetomm/dotnet-options-pattern-benchmark/blob/main/Program.cs#L69)
Represents the use of an section of appsettings via Monitor pattern. This pattern only reads the current value of Monitor instance to get the inhected class. In this cenario, appsettings file isn't changed.

### [EscritaEmDisco_E_LeituraMonitor](https://github.com/felipetomm/dotnet-options-pattern-benchmark/blob/main/Program.cs#L78)
Represents the use of Monitor with changes in appsettings file, just to see the sincronization of appsettings values into the monitored class. It'll be impacted by Disk I/O.

## Related links
[Oğuzhan Ağır - The ASP.NET Core Dependency Injection System - The Options Pattern](https://abp.io/community/articles/the-asp.net-core-dependency-injection-system-3vbsdhq8#the-options-pattern-ioptionst-ioptionssnapshott-ioptionsmonitort)
