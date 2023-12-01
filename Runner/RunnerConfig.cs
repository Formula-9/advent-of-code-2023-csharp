using System.Net;
using Microsoft.Extensions.Configuration;

namespace Formula9.AdventOfCode.Runner;

public class RunnerConfig
{
    public static RunnerConfig Instance => _lazy.Value;
    private IConfigurationRoot ConfigurationRoot { get; init; }
    private static readonly Lazy<RunnerConfig> _lazy = new(new RunnerConfig());

    private RunnerConfig()
    {
        ConfigurationRoot = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory).AddJsonFile("config.json").Build();
    }

    public Cookie GetAdventOfCodeCookie() => new("session", ConfigurationRoot["cookie"]);
}