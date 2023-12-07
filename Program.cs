using AoCHelper;
using Formula9.AdventOfCode.Solutions2023.Day07;

namespace Formula9.AdventOfCode;

public class Program
{
    public static void Main(string[] args)
    {
        Type day = typeof(Day_07);
        Solver.Solve(options => {
            options.ShowTotalElapsedTimePerDay = false;
            options.ShowOverallResults = false;
            options.ClearConsole = true;
            options.ElapsedTimeFormatSpecifier = "F3";
        }, day).Wait();
    }
}