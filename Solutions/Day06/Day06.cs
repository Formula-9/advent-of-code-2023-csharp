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
        FinalRound = (long.Parse(string.Join(string.Empty, GetNumbersForLine(lines.First()))), long.Parse(string.Join(string.Empty, GetNumbersForLine(lines.Last()))));
    }

    private static IEnumerable<long> GetNumbersForLine(string s) =>s
        .Split(":", StringSplitOptions.RemoveEmptyEntries)
        .Last()
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(long.Parse);

    public static long CountSolutions((long time, long distance) pair)
    {
        long solutions = 0;
        for (long timeSpentHoldingButton = 0; timeSpentHoldingButton < pair.time; timeSpentHoldingButton++)
        {
            // i = speed
            long remainingTime = pair.time - timeSpentHoldingButton;
            solutions += ((remainingTime * timeSpentHoldingButton) > pair.distance).ToInt();
        }
        return solutions;
    }

    public override ValueTask<string> Solve_1() => new(TimeDistancePairs.Select(CountSolutions).Aggregate((acc, el) => acc * el).ToString());

    public override ValueTask<string> Solve_2() => new(CountSolutions(FinalRound).ToString());
}