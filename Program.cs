using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<OptionsBenchmark>();
    }
}

[MemoryDiagnoser]
public class OptionsBenchmark
{
    private IOptionsMonitor<MinhaConfig> _monitor;
    private MinhaConfig _singletonInstance;
    private const string ConfigFileName = "appsettings.json";
    private int _contador = 0; // Para mudar o valor a cada escrita

    [GlobalSetup]
    public void Setup()
    {
        // Garante um arquivo inicial válido
        GravarArquivoJson(0);

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(ConfigFileName, optional: false, reloadOnChange: true);
        
        var configuration = builder.Build();
        var services = new ServiceCollection();
        services.Configure<MinhaConfig>(configuration.GetSection("MinhaConfig"));
        var serviceProvider = services.BuildServiceProvider();

        _monitor = serviceProvider.GetRequiredService<IOptionsMonitor<MinhaConfig>>();
        _monitor.OnChange(settings =>
        {
            Console.WriteLine($"Configuration changed! New value: {settings.Valor}");
        });
        
        _singletonInstance = new MinhaConfig { Nome = "Teste", Valor = 123 };
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        if (File.Exists(ConfigFileName)) File.Delete(ConfigFileName);
    }

    // Helper para escrever no disco
    private void GravarArquivoJson(int valor)
    {
        var jsonContent = $"{{ \"MinhaConfig\": {{ \"Nome\": \"Teste {valor}\", \"Valor\": {valor} }} }}";
        File.WriteAllText(ConfigFileName, jsonContent);
    }

    // 1. BASELINE: Acesso a memória pura
    [Benchmark(Baseline = true)]
    public string AcessoSingletonPuro()
    {
        return _singletonInstance.Nome;
    }

    // 2. O TESTE REAL: Acesso via Monitor (apenas leitura)
    [Benchmark]
    public string AcessoOptionsMonitor()
    {
        return _monitor.CurrentValue.Nome;
    }

    // 3. O CENÁRIO DE "CAOS": Escrever no arquivo e ler em seguida
    // Este teste vai ser "LENTO" por causa do File.WriteAllText, não do Monitor.
    // Isso simula o custo de fazer o deploy/salvar o arquivo.
    [Benchmark]
    public string EscritaEmDisco_E_LeituraMonitor()
    {
        _contador++;
        
        // if(_contador % 10 == 0)
        GravarArquivoJson(_contador); // O gargalo está aqui (Disco)
        
        // O Monitor não bloqueia aqui. Ele retorna o valor (mesmo que seja o antigo por alguns ms)
        // instantaneamente, sem esperar o reload do arquivo terminar.
        return _monitor.CurrentValue.Nome; 
    }
}

public class MinhaConfig
{
    public string Nome { get; set; }
    public int Valor { get; set; }
}