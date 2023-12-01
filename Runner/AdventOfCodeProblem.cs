using AoCHelper;
using Formula9.AdventOfCode.Runner;

namespace Formula9.AdventOfCode.Utils;

public abstract class AdventOfCodeProblem : BaseDay
{
    public string Input { get; init; }

    public AdventOfCodeProblem(int year, int day)
    {
        Input = File.Exists(InputFilePath) ? File.ReadAllText(InputFilePath) : AdventOfCodeFetcher.Instance.GetInputFromWeb(year, day, InputFilePath);
    }
}