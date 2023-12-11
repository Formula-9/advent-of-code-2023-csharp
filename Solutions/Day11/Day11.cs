using Formula9.AdventOfCode.Utils;

namespace Formula9.AdventOfCode.Solutions2023.Day11;

public class Day_11 : AdventOfCodeProblem
{

    public ISet<int> EmptyRows { get; init; }
    public ISet<int> EmptyCols { get; init; }
    public ISet<(int y, int x)> Stars { get; init; }
    public ISet<((int y, int x) left, (int y, int x) right)> StarPairs { get; private set; }

    public Day_11() : base(2023, 11) 
    {
        EmptyRows   = new HashSet<int>();
        EmptyCols   = new HashSet<int>();
        Stars       = new HashSet<(int y, int x)>();
        StarPairs   = new HashSet<((int y, int x) left, (int y, int x) right)>();
        ReadData();
    }

    public void ReadData()
    {
        var grid    = Input.SplitByNewline();
        foreach ((int y, var line) in grid.Enumerate())
        {
            if (IsLineEmpty(line))
            {
                EmptyRows.Add(y);
                continue;
            }
            
            foreach ((int x, char c) in line.Enumerate())
            {
                if (c == '#') Stars.Add((y, x));
            }
        }

        foreach (int x in Enumerable.Range(0, grid[0].Length))
        {
            if (Enumerable.Range(0, grid.Length).Select(y => grid[y][x]).ToHashSet().Count == 1)
            {
                EmptyCols.Add(x);
            }
        }

        StarPairs = Stars.Combinations(2).Select(enumerable => (left: enumerable.First(), right: enumerable.Last())).ToHashSet();
    }

    public long FindShortestPath((int x, int y) left, (int x, int y) right, int expansionLevel)
    {
        (int smallestX, int smallestY) = (Math.Min(left.x, right.x), Math.Min(left.y, right.y));
        (int biggestX,  int biggestY)  = (Math.Max(left.x, right.x), Math.Max(left.y, right.y));
        (long dx, long dy) = (Math.Abs(left.x - right.x), Math.Abs(left.y - right.y));
        foreach (var row in EmptyRows)
        {
            for (int x = smallestX; x < biggestX; x++)
            {
                if (row == x)
                {
                    dx += expansionLevel;
                    break;
                }
            }
        }
        foreach (var col in EmptyCols)
        {
            for (int y = smallestY; y < biggestY; y++)
            {
                if (col == y)
                {
                    dy += expansionLevel;
                    break;
                }
            }
        }
        return dx + dy;
    } 

    public static bool IsLineEmpty(string l) => l.Select(c => c).ToHashSet().Count == 1;

    public override ValueTask<string> Solve_1() => new(StarPairs.Sum(pairTuple => FindShortestPath(pairTuple.left, pairTuple.right, 1)).ToString());

    public override ValueTask<string> Solve_2() => new(StarPairs.Sum(pairTuple => FindShortestPath(pairTuple.left, pairTuple.right, 1_000_000 - 1)).ToString());
}