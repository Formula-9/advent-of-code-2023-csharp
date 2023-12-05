using Formula9.AdventOfCode.Utils;

namespace Formula9.AdventOfCode.Solutions2023.Day05;

public class Day_05 : AdventOfCodeProblem
{
    public List<long> Seeds { get; init; }
    public List<List<(long, long, long)>> Mappers { get; init; }

    public Day_05() : base(2023, 05) 
    { 
        var paragraphs = Input.SplitByParagraph();
        Seeds = ReadSeeds(paragraphs.First());
        Mappers = paragraphs.Skip(1).Select(p => p.SplitByNewline().Skip(1).Select(LineToTuple).ToList()).ToList();
    }

    private static ValueTuple<long, long, long> LineToTuple(string s)
    {
        var data = s.Split(" ").Select(long.Parse).ToArray();
        return new(data[0], data[1], data[2]);
    }
    private static List<long> ReadSeeds(string s) => s
            .Split(":", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Last()
            .Split(" ")
            .Select(long.Parse)
            .ToList();

    private static bool IsMapped((long dest, long src, long rng) mapper, long seed) => seed >= mapper.src && seed < mapper.src + mapper.rng - 1;

    private static long MapValue((long dest, long src, long rng) mapper, long seed) => mapper.dest + seed - mapper.src;

    public long PerformFullMapping(long seed) => Mappers.Aggregate(seed, (seed, mapList) => mapList.Any(m => IsMapped(m, seed), out var _m) ? MapValue(_m, seed) : seed);

    public override ValueTask<string> Solve_1() => new(Seeds.Min(PerformFullMapping).ToString());

    public override ValueTask<string> Solve_2()
    {
        var ranges = Seeds.Chunk(2).Select(a => new ValueTuple<long, long>(a[0], a[0] + a[1] - 1)).ToList();
        foreach (var mapList in Mappers) {
            var newRanges = new List<(long, long)>();
            foreach ((long low, long high) in ranges)
            {
                foreach ((long dest, long src, long rng) in mapList)
                {
                    if (high < src || low > src + rng - 1) continue;
                    newRanges.Add((Math.Max(low, src) + dest - src, Math.Min(high, src + rng - 1) + dest - src));
                }
            }
            ranges = newRanges;
        }
        var best = ranges.Min(t => t.Item1).ToString();
        return new(best);
    }
}