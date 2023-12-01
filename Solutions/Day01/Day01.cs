using System.Collections.Immutable;
using Formula9.AdventOfCode.Utils;

namespace Formula9.AdventOfCode.Solutions2023.Day01;

public class Day_01 : AdventOfCodeProblem
{
    public Day_01() : base(2023, 1) { }
    
    private static readonly IDictionary<string, int> Digits = new Dictionary<string, int>()
    {
        { "one",    1 },
        { "two",    2 },
        { "three",  3 },
        { "four",   4 },
        { "five",   5 },
        { "six",    6 },
        { "seven",  7 },
        { "eight",  8 },
        { "nine",   9 }
    }.ToImmutableDictionary();

    private enum MatchType
    {
        None,
        Digit,
        ThreeLetterDigit,
        FourLetterDigit,
        FiveLetterDigit
    }

    private readonly record struct DigitMatch(MatchType Type, string String);

    private static DigitMatch FindMatch(string s)
    {
        if (char.IsDigit(s.First()))                        return new(MatchType.Digit,             new string(s[0], 1));
        if (s.Length >= 5 && Digits.ContainsKey(s[..5]))    return new(MatchType.FiveLetterDigit,   s[..5]);
        if (s.Length >= 4 && Digits.ContainsKey(s[..4]))    return new(MatchType.FourLetterDigit,   s[..4]);
        if (s.Length >= 3 && Digits.ContainsKey(s[..3]))    return new(MatchType.ThreeLetterDigit,  s[..3]);
        return new(MatchType.None, string.Empty);
    }
    
    private static int ConvertToDigit(DigitMatch m) => m.Type == MatchType.Digit ? m.String.First() - '0' : Digits[m.String];

    private static int FindDigitPair(string s)
    {
        List<DigitMatch> matches = new();
        for (int i = 0; i < s.Length; i++)
        {
            var match = FindMatch(s[i..]);
            if (match.Type != MatchType.None)
            {
                matches.Add(match);
            }
        }
        return ConvertToDigit(matches.First()) * 10 + ConvertToDigit(matches.Last());
    }

    public override ValueTask<string> Solve_1() => new(Input.SplitByNewline().Select(l => int.Parse(new char[] { l.First(char.IsDigit), l.Last(char.IsDigit) })).Sum().ToString());

    public override ValueTask<string> Solve_2() => new(Input.SplitByNewline().Select(FindDigitPair).Sum().ToString());
}