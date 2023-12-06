using Formula9.AdventOfCode.Utils;

namespace Formula9.AdventOfCode.Solutions2023.Day06;

public class Day_06 : AdventOfCodeProblem
{

    List<(long time, long distance)> TimeDistancePairs { get; init; }
    (long time, long distance) FinalRound { get; init; }

    public Day_06() : base(2023, 06) 
    { 
        var lines = Input.SplitByNewline().ToArray();
        TimeDistancePairs = GetNumbersForLine(lines.First()).Zip(GetNumbersForLine(lines.Last())).ToList();
        var finalRoundData = lines.Select(l => long.Parse(l.Replace(" ", string.Empty).Split(":").Last())).ToArray();
        FinalRound = new(finalRoundData.First(), finalRoundData.Last());
    }

    private static IEnumerable<long> GetNumbersForLine(string s) =>s
        .Split(":", StringSplitOptions.RemoveEmptyEntries)
        .Last()
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(long.Parse);

    public static long CountSolutions((long time, long distance) pair)
    {
        double firstRoot  = Math.Ceiling((-pair.time - Math.Sqrt(pair.time * pair.time - 4.0 * pair.distance)) / 2f + 0.01f);
        double secondRoot = Math.Floor((-pair.time + Math.Sqrt(pair.time * pair.time - 4.0 * pair.distance)) / 2f - 0.01f);
        return (long)(Math.Abs(secondRoot - firstRoot) + 1);
    }

    public override ValueTask<string> Solve_1() => new(TimeDistancePairs.Select(CountSolutions).Aggregate((acc, el) => acc * el).ToString());

    public override ValueTask<string> Solve_2() => new(CountSolutions(FinalRound).ToString());
}