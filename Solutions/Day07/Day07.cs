using Formula9.AdventOfCode.Utils;

namespace Formula9.AdventOfCode.Solutions2023.Day07;

public class Day_07 : AdventOfCodeProblem
{
    public List<Hand> Hands { get; init; }

    public Day_07() : base(2023, 07) 
    { 
        Hands = Input.SplitByNewline().Select(FromString).ToList();
    }

    private static Hand FromString(string s)
    {
        var data = s.Split(" ");
        return new(data.First(), long.Parse(data.Last()));
    }

    public override ValueTask<string> Solve_1() => new(Hands.AsEnumerable().Sort(new HandComparer()).Select((h, index) => h.Bid * (index + 1)).Sum().ToString());

    // 252137472
    public override ValueTask<string> Solve_2() => new("Solution 2");
}

public class HandComparer : IComparer<Hand>
{
    public const string Cards = "23456789TJQKA";

    public int Compare(Hand x, Hand y)
    {
        int comparisonResult = x.GetHandType().CompareTo(y.GetHandType());
        if (comparisonResult == 0)
        {
            foreach ((int leftCard, int rightCard) in x.Cards.Select(c => Cards.IndexOf(c)).Zip(y.Cards.Select(c => Cards.IndexOf(c))))
            {
                comparisonResult = leftCard.CompareTo(rightCard);
                if (comparisonResult != 0) break;       
            }
        }
        return comparisonResult;
    }
}

public class PartTwoHandComparer : IComparer<Hand>
{
    public const string Cards = "J23456789TQKA";

    public int Compare(Hand x, Hand y)
    {
        int comparisonResult = x.GetHandTypeForPartTwo().CompareTo(y.GetHandTypeForPartTwo());
        if (comparisonResult == 0)
        {
            foreach ((int leftCard, int rightCard) in x.Cards.Select(c => Cards.IndexOf(c)).Zip(y.Cards.Select(c => Cards.IndexOf(c))))
            {
                comparisonResult = leftCard.CompareTo(rightCard);
                if (comparisonResult != 0) break;       
            }
        }
        return comparisonResult;
    }
}

public enum HandType
{
    HighCard = 0,
    OnePair,
    TwoPair,
    ThreeOfAKind,
    FullHouse,
    FourOfAKind,
    FiveOfAKind,
}

public readonly record struct Hand(string Cards, long Bid)
{
    public HandType GetHandType()
    {
        var groups = Cards.GroupBy(x => x).ToList();
        if (groups.Count == 1) return HandType.FiveOfAKind;
        if (groups.Count == 2) return groups.Any(x => x.Count() == 4) ? HandType.FourOfAKind  : HandType.FullHouse;
        if (groups.Count == 3) return groups.Any(x => x.Count() == 3) ? HandType.ThreeOfAKind : HandType.TwoPair;
        return groups.Any(x => x.Count() == 2) ? HandType.OnePair : HandType.HighCard;
    }

    public HandType GetHandTypeForPartTwo()
    {
        int replacementCards = Cards.Count(c => c == 'J');
        var groups = Cards.GroupBy(x => x).ToList();
        return 0;
    }
}