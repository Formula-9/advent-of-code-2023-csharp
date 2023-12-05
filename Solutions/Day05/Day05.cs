using Formula9.AdventOfCode.Utils;

namespace Formula9.AdventOfCode.Solutions2023.Day05;

public class Day_05 : AdventOfCodeProblem
{
    public List<long> Seeds { get; init; }
    public List<SeedRange> SeedRanges { get; init; }
    public List<string> Headers { get; init; }
    public Dictionary<string, List<Mapper>> Mappers { get; init; }

    public Day_05() : base(2023, 05) 
    { 
        var paragraphs = Input.SplitByParagraph();
        Seeds = ReadSeeds(paragraphs.First());
        SeedRanges = ReadSeedRanges(paragraphs.First());
        Headers = new();
        Mappers = new();
        foreach (var paragraph in paragraphs.Skip(1))
        {
            var lines = paragraph.SplitByNewline();
            var header = string.Join(string.Empty, lines.First().TakeWhile(c => !char.IsWhiteSpace(c)));
            Headers.Add(header);
            Mappers[header] = lines.Skip(1).Select(FromLine).ToList();
        }
    }

    private static List<long> ReadSeeds(string s) => s[7..].Split(" ").Select(long.Parse).ToList();

    private static List<SeedRange> ReadSeedRanges(string s) => s[7..].Split(" ").Select(long.Parse).Chunk(2).Select(p => new SeedRange(p[0], p[0] + p[1])).ToList();

    private static Mapper FromLine(string s)
    {
        var data = s.Split(" ").Select(long.Parse).ToArray();
        return new(data[0], data[1], data[2]);
    }

    public long PerformFullMapping(long seed)
    {
        return Headers.Aggregate(seed, (seed, header) => Mappers[header].Any(m => m.IsMapped(seed), out Mapper mapper) ? mapper.MapValue(seed) : seed);
    }
    
    public override ValueTask<string> Solve_1() => new(Seeds.Select(PerformFullMapping).Min().ToString());

    public override ValueTask<string> Solve_2()
    {
        return new("");
    }
}

public readonly record struct SeedRange(long Start, long Length)
{
    public readonly long End => Start + Length;
}

public readonly record struct Mapper(long DestinationStart, long RangeStart, long RangeLength)
{
    public readonly long RangeEnd => RangeStart + RangeLength;
    public readonly bool IsMapped(long sourceValue) => sourceValue >= RangeStart && sourceValue < RangeEnd;
    public readonly long MapValue(long sourceValue) => IsMapped(sourceValue) ? DestinationStart + sourceValue - RangeStart : sourceValue;

}