using Formula9.AdventOfCode.Utils;

namespace Formula9.AdventOfCode.Solutions2023.Day09;

public class Day_09 : AdventOfCodeProblem
{
    public List<List<long>> Histories { get; init; }

    public Day_09() : base(2023, 09) 
    { 
        Histories = Input.SplitByNewline().Select(l => l.Split(" ").Select(long.Parse).ToList()).ToList();
    }

    public static long Project(IEnumerable<long> values, int indexToExclude)
    {
        int n = values.Count();
        return (long)Math.Round(Enumerable.Range(0, n).Where(n => n != indexToExclude).Product(k => (double)(n - k) / (indexToExclude - k)));
    }

    public static double LagrangeInterpolatingPolynomial(IEnumerable<long> values) => values.Sum((v, index) => v * Project(values, index));
 
    public override ValueTask<string> Solve_1() => new(Histories.Sum(LagrangeInterpolatingPolynomial).ToString());

    public override ValueTask<string> Solve_2() => new(Histories.Sum(h => LagrangeInterpolatingPolynomial(h.AsEnumerable().Reverse())).ToString());
}