using System.Text;
using Formula9.AdventOfCode.Utils;

namespace Formula9.AdventOfCode.Solutions2023.Day03;

public class Day_03 : AdventOfCodeProblem
{

    public char[,] Grid { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public HashSet<Number> Numbers { get; set; }
    public HashSet<Symbol> Stars { get; set; }

    public Day_03() : base(2023, 03) 
    { 
        FindNumbers();
    }

    public void FindNumbers()
    {
        string[] lines = Input.SplitByNewline();
        (Height, Width) = (lines.Length, lines.First().Length);
        Numbers = new();
        Stars = new();
        Grid = new char[Height, Width];
        for (int y = 0; y < lines.Length; y++)
        {
            StringBuilder numberBuilder = new();
            (int startX, int startY) = (-1, -1);
            for (int x = 0; x < lines[y].Length; x++)
            {
                char c = lines[y][x];
                if (char.IsDigit(c))
                {
                    numberBuilder.Append(c);
                    if (startX == -1 && startY == -1)
                    {
                        (startX, startY) = (x, y);
                    }
                }
                else if (numberBuilder.Length > 0)
                {
                    string n = numberBuilder.ToString();
                    Numbers.Add(new Number(startX, startY, int.Parse(n), n.Length));
                    numberBuilder.Clear();
                    (startX, startY) = (-1, -1);
                }

                if (c == '*')
                {
                    Stars.Add(new Symbol(c, x, y));
                }
                Grid[y, x] = c;
            }

            if (numberBuilder.Length > 0)
            {
                string n = numberBuilder.ToString();
                Numbers.Add(new Number(startX, startY, int.Parse(n), n.Length));
            }
        }
    }

    public bool IsNumberEnginePart(Number n) => FindNeighbors(n.X, n.Y, n.Length).Any(x => !char.IsDigit(x.Character) && x.Character != '.');

    public IEnumerable<Symbol> FindNeighbors(int x, int y, int length)
    {
        List<Symbol> chars = new();
        if (y > 0)
        {
            chars.AddRange(Enumerable.Range(x, length).Select(x => new Symbol(Grid[y - 1, x], x, y - 1)));
        }
        if (y < Height - 1)
        {
            chars.AddRange(Enumerable.Range(x, length).Select(x => new Symbol(Grid[y + 1, x], x, y + 1)));
        }
        if (x > 0)
        {
            chars.AddRange(Enumerable.Range(y - (y > 0).ToInt(), 1 + (y > 0).ToInt() + (y < Height - 1).ToInt()).Select(y => new Symbol(Grid[y, x - 1], x - 1, y)));
        }
        if (x + length < Width)
        {
            chars.AddRange(Enumerable.Range(y - (y > 0).ToInt(), 1 + (y > 0).ToInt() + (y < Height - 1).ToInt()).Select(y => new Symbol(Grid[y, x + length], x + length, y)));
        }
        return chars;
    }

    public override ValueTask<string> Solve_1() => new(Numbers.Where(IsNumberEnginePart).Sum(x => x.Value).ToString());

    public int FindRatios(Symbol s)
    {
        List<Symbol> neighbors = FindNeighbors(s.X, s.Y, 1).Where(s => char.IsDigit(s.Character)).ToList();
        List<Number> hash      = Numbers.Where(n => neighbors.Any(neighbor => n.IsPositionPartOfNumber(neighbor.X, neighbor.Y))).ToList();
        int value = hash.Count == 2 ? hash.First().Value * hash.Last().Value : 0;
        return value;
    }

    public override ValueTask<string> Solve_2()
    {
        return new(Stars.Select(FindRatios).Sum().ToString());
    }
}

public readonly record struct Number(int X, int Y, int Value, int Length)
{
    public bool IsPositionPartOfNumber(int x, int y) => Y == y && x >= X && x < X + Length;
}

public readonly record struct Symbol(char Character, int X, int Y);