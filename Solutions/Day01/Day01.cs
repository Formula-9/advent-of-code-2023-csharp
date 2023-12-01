using Formula9.AdventOfCode.Utils;

namespace Formula9.AdventOfCode.Solutions2023.Day01;

public class Day_01 : AdventOfCodeProblem
{
    private string[] SplitInput { get; init; }

    public Day_01() : base(2023, 1) 
    { 
        SplitInput = Input.SplitByNewline();
    }

    private static readonly IDictionary<string, string> Digits = new Dictionary<string, string>()
    {
        { "one",    "o1e" },
        { "two",    "t2o" },
        { "three",  "t3e" },
        { "four",   "f4r" },
        { "five",   "f5e" },
        { "six",    "s6x" },
        { "seven",  "s7n" },
        { "eight",  "e8t" },
        { "nine",   "n9e" }
    };

    private static int FindDigitPair(string s) => SumFirstAndLastDigit(s.ReplaceAll(Digits));

    private static int SumFirstAndLastDigit(string s) => (s.First(char.IsDigit) - '0') * 10 + (s.Last(char.IsDigit) - '0');

    public override ValueTask<string> Solve_1() => new(SplitInput.Select(SumFirstAndLastDigit).Sum().ToString());

    public override ValueTask<string> Solve_2() => new(SplitInput.Select(FindDigitPair).Sum().ToString());
}