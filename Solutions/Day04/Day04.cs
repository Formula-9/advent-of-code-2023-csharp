using Formula9.AdventOfCode.Utils;

namespace Formula9.AdventOfCode.Solutions2023.Day04;

public class Day_04 : AdventOfCodeProblem
{

    public List<Scratchcard> Cards { get; init; }

    public Day_04() : base(2023, 04) 
    { 
        Cards = Input.SplitByNewline().Select(ReadCard).ToList();
    }

    public static Scratchcard ReadCard(string s)
    {
        var values = s.Split(':').Last().Split('|');
        var winningNumbers = values.First().Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToHashSet();
        var cardNumbers = values.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToList();
        return new(winningNumbers, cardNumbers);
    }

    public override ValueTask<string> Solve_1() => new(Cards.Sum(c => c.GetValue()).ToString());

    public override ValueTask<string> Solve_2()
    {
        // Dictionary mapping index of a card to its number of copies
        var copies = Enumerable.Range(0, Cards.Count).ToDictionary(index => index, _ => 1);
        for (int i = 0; i < Cards.Count; i++)
        {
            var card = Cards[i];
            int matches = card.GetMatches();
            int j = i + 1;
            while (j < Cards.Count && j <= i + matches)
            {
                copies[j] += copies[i];
                j++;
            }
        }
        return new(copies.Sum(kvp => kvp.Value).ToString());
    }
}

public record class Scratchcard(HashSet<int> WinningNumbers, List<int> CardNumbers)
{
    public int GetMatches() => CardNumbers.Sum(n => WinningNumbers.Contains(n).ToInt());
    public int GetValue() => (int)Math.Pow(2, GetMatches() - 1);
}