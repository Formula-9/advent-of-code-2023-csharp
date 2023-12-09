using Formula9.AdventOfCode.Utils;

namespace Formula9.AdventOfCode.Solutions2023.Day08;

public class Day_08 : AdventOfCodeProblem
{
    public string Instructions { get; init; }

    public Dictionary<string, (string left, string right)> Graph { get; init; }

    public Day_08() : base(2023, 08) 
    { 
        var paragraphs = Input.SplitByParagraph();
        Instructions = paragraphs.First();
        Graph = new();
        foreach (var line in paragraphs.Last().SplitByNewline())
        {
            var entries = line.Split("=", StringSplitOptions.TrimEntries);
            var toEntries = entries.Last()[1..^1].Split(", ");
            Graph[entries.First()] = (toEntries.First(), toEntries.Last());
        }
    }

    public long LoopLength(string start)
    {
        (string step, string stepPlusOne) = (start, start);
        long count = 1;
        using var stepPlusOneEnumerator = Instructions.RepeatInfinitely().GetEnumerator();
        foreach (var direction in Instructions.RepeatInfinitely())
        {
            step = direction == 'L' ? Graph[step].left : Graph[step].right;
            for (int i = 0; i < 2; i++)
                stepPlusOne = stepPlusOneEnumerator.Next() == 'L' ? Graph[stepPlusOne].left : Graph[stepPlusOne].right;
            if (step == stepPlusOne && (count % Instructions.Length) == (count * 2 % Instructions.Length))
                break;
            count++;
        }
        return count;
    }

    public override ValueTask<string> Solve_1()
    {
        (string position, int instructionIndex, int steps) = ("AAA", 0, 0);
        while (position != "ZZZ")
        {
            position = Instructions[instructionIndex] == 'L' ? Graph[position].left : Graph[position].right;
            (instructionIndex, steps) = ((instructionIndex + 1) % Instructions.Length, steps + 1);
        }
        return new(steps.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        HashSet<string> startPoints = Graph.Where(kvp => kvp.Key.EndsWith("A")).Select(kvp => kvp.Key).ToHashSet();
        long steps = MathUtils.Lcm(startPoints.Select(LoopLength).ToArray());
        return new(steps.ToString());
    }
}