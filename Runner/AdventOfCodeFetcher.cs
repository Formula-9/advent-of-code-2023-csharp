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
        ThrowIfPuzzleIsNotRevealedYet(year, day);
        string result = FetchInputForDayFromWeb(year, day);
        if (!string.IsNullOrEmpty(saveToPath))
        {
            string? dirName = Path.GetDirectoryName(saveToPath);
            if (dirName != null && !Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }
            using var stream = File.OpenWrite(saveToPath);
            using var streamWriter = new StreamWriter(stream);
            streamWriter.WriteLine(result);
        }
        return result;
    }

    private static void ThrowIfPuzzleIsNotRevealedYet(int year, int day)
    {
        var date = DateTime.UtcNow;
        if (year > date.Year || (year == date.Year && day > date.Day) || (year == date.Year && day == date.Day && date.Hour < 5))
        {
            throw new PuzzleNotRevealedExceptionException();
        }
    }

    private string FetchInputForDayFromWeb(int year, int day)
    {
        var response = Client.GetAsync($"https://adventofcode.com/{year}/day/{day}/input").Result;
        return response.IsSuccessStatusCode ? response.Content.ReadAsStringAsync().Result : throw new HttpRequestException("Failed to fetch content.");
    }
}