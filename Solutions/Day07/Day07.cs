using Formula9.AdventOfCode.Utils;

namespace Formula9.AdventOfCode.Solutions2023.Day07;

public class Day_07 : AdventOfCodeProblem
{
    public const string Cards = "23456789TJQKA";
    public const string PartTwoCardValues = "J23456789TQKA";

    public Day_07() : base(2023, 07) { }

    private static Hand FromString(string s, string cardValues)
    {
        var data = s.Split(" ");
        var nums = data.First().Select(c => cardValues.IndexOf(c) + 1).ToArray();
        var groups = nums.GroupBy(n => n).OrderByDescending(g => g.Count()).Select(ToGroup).ToArray();
        return new(Values: nums, Groups: groups, Bid: long.Parse(data.Last()));
    }

    public static char ReverseMap(int val) => PartTwoCardValues.ElementAt(val - 1);

    public static CardGroup ToGroup(IGrouping<int, int> group) => new(group.Key, group.Count());

    public static int GetHandType(Hand hand, bool discardJokers = false)
    {
        int[] groups;
        if (discardJokers && hand.Groups.Any(g => g.Value == 1 && g.Count >= 1 && g.Count <= 4, out var discardedGroup))
        {
            groups = hand.Groups.Where(x => x.Value > 1).Select(x => x.Count).OrderDescending().ToArray();
            if (groups.Length > 0) groups[0] += discardedGroup.Count;
        }
        else
        {
            groups = hand.Groups.Select(x => x.Count).OrderDescending().ToArray();
        }
        return groups switch
        {
            [5]             => 7,
            [4, _]          => 6,
            [3, 2]          => 5,
            [3, _, _]       => 4,
            [2, 2, _]       => 3,
            [2, 2]          => 3,
            [2, _, _, _]    => 2,
            [2, _, _]       => 2,
            [2, _]          => 2,
            _               => 1
        };
    }

    public override ValueTask<string> Solve_1()
    {
        var value = Input
            .SplitByNewline()
            .Select(l => FromString(l, Cards))
            .OrderBy(h => GetHandType(h))
            .ThenBy(h => h.Values, new IntArrayComparer())
            .Sum((h, i) => h.Bid * (i + 1));
        return new(value.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var value = Input
            .SplitByNewline()
            .Select(l => FromString(l, PartTwoCardValues))
            .OrderBy(h => GetHandType(h, discardJokers: true))
            .ThenBy(h => h.Values, new IntArrayComparer())
            .Sum((h, i) => h.Bid * (i + 1));
        return new(value.ToString());
    }
}

class IntArrayComparer : IComparer<int[]>
{
    public int Compare(int[]? x, int[]? y)
    {
        foreach (var (l, r) in x.Zip(y))
        {
            int result = l.CompareTo(r);
            if (result != 0) return result;
        }
        return 0;
    }
}

public readonly record struct Hand(int[] Values, CardGroup[] Groups, long Bid);

public readonly record struct CardGroup(int Value, int Count);