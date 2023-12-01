using System.Collections.Immutable;
using Formula9.AdventOfCode.Utils;

namespace Formula9.AdventOfCode.Solutions2023.Day01;

public class Day_01 : AdventOfCodeProblem
{
    public Day_01() : base(2023, 1) { }

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
    }.ToImmutableDictionary();

    private static int FindDigitPair(string s)
    {
        string altered  = s.ReplaceAll(Digits);
        char first      = altered.First(char.IsDigit);
        char last       = altered.Last(char.IsDigit);
        return (first - '0') * 10 + (last - '0');
    }

    public override ValueTask<string> Solve_1() => new(Input.SplitByNewline().Select(l => int.Parse(new char[] { l.First(char.IsDigit), l.Last(char.IsDigit) })).Sum().ToString());

    public override ValueTask<string> Solve_2() => new(Input.SplitByNewline().Select(FindDigitPair).Sum().ToString());
}