using System.Text.RegularExpressions;
using Formula9.AdventOfCode.Utils;

namespace Formula9.AdventOfCode.Solutions2023.Day02;

public partial class Day_02 : AdventOfCodeProblem
{
    public Day_02() : base(2023, 2) {}

    public readonly string[] Separators = new string[] { ":", ";" };
    public readonly Regex RoundRegex = MyRegex();
    [GeneratedRegex(@"(\d+)(?= (red|green|blue))", RegexOptions.Compiled)]
    private static partial Regex MyRegex();

    public int IsPossible(string line)
    {
        bool isPossible = true;
        var data        = line.Split(Separators, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var rounds      = data.Skip(1);
        foreach (string round in rounds)
        {
            if (!IsRoundPossible(round))
            {
                isPossible = false;
                break;
            }
        }
        return isPossible ? int.Parse(data.First()[5..]) : 0;
    }

    public bool IsRoundPossible(string round)
    {
        bool isRoundPossible = true;
        foreach (Match match in RoundRegex.Matches(round))
        {
            isRoundPossible = match.Groups[2].ValueSpan switch
            {
                "red"   => int.Parse(match.Groups[1].ValueSpan) <= 12,
                "green" => int.Parse(match.Groups[1].ValueSpan) <= 13,
                "blue"  => int.Parse(match.Groups[1].ValueSpan) <= 14,
                _       => throw new Exception($"Got values 1 = {match.Groups[1].ValueSpan} & 2 = {match.Groups[2].ValueSpan}"),
            };
            if (!isRoundPossible) break;
        }
        return isRoundPossible;
    }

    public int Powers(string line)
    {
        var (red, green, blue) = line
                                    .Split(Separators, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                                    .Skip(1)
                                    .Aggregate((red: 0, green: 0, blue: 0), MinNumber);
        return red * blue * green;
    }

    public (int red, int green, int blue) MinNumber((int red, int green, int blue) prev, string round)
    {
        (int roundRed, int roundGreen, int roundBlue) = (0, 0, 0);
        foreach (Match match in RoundRegex.Matches(round))
        {
            switch (match.Groups[2].ValueSpan )
            {
                case "red":
                    roundRed   = int.Parse(match.Groups[1].ValueSpan);
                    break;
                case "green":
                    roundGreen = int.Parse(match.Groups[1].ValueSpan);
                    break;
                case "blue":
                    roundBlue  = int.Parse(match.Groups[1].ValueSpan);
                    break;
                default:
                    throw new Exception($"Got values 1 = {match.Groups[1].ValueSpan} & 2 = {match.Groups[2].ValueSpan}");
            };
        }
        return (red: Math.Max(roundRed, prev.red), green: Math.Max(roundGreen, prev.green), blue: Math.Max(roundBlue, prev.blue));
    }

    public override ValueTask<string> Solve_1() => new(Input.SplitByNewline().Sum(IsPossible).ToString());

    public override ValueTask<string> Solve_2() => new(Input.SplitByNewline().Sum(Powers).ToString());
}