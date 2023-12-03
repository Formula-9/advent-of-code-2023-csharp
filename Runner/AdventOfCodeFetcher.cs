namespace Formula9.AdventOfCode.Runner;

public class AdventOfCodeFetcher
{
    public static AdventOfCodeFetcher Instance => _lazy.Value;
    public HttpClient Client { get; init; }
    public HttpClientHandler ClientHandler { get; init; }

    private static readonly Lazy<AdventOfCodeFetcher> _lazy = new(new AdventOfCodeFetcher());

    private AdventOfCodeFetcher()
    {
        ClientHandler = new();
        ClientHandler.CookieContainer.Add(RunnerConfig.Instance.GetAdventOfCodeCookie());
        Client = new HttpClient(ClientHandler);
    }

    public string GetInputFromWeb(int year, int day, string saveToPath)
    {
        string result = FetchInputForDayFromWeb(year, day);
        if (!string.IsNullOrEmpty(saveToPath))
        {
            string dirName = Path.GetDirectoryName(saveToPath);
            if (!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }
            using var stream = File.OpenWrite(saveToPath);
            using var streamWriter = new StreamWriter(stream);
            streamWriter.WriteLine(result);
        }
        return result;
    }

    private string FetchInputForDayFromWeb(int year, int day)
    {
        var response = Client.GetAsync($"https://adventofcode.com/{year}/day/{day}/input").Result;
        return response.IsSuccessStatusCode ? response.Content.ReadAsStringAsync().Result : throw new HttpRequestException("Failed to fetch content.");
    }
}