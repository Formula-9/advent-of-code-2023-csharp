using AoCHelper;
using Formula9.AdventOfCode.Solutions2023.Day03;

namespace Formula9.AdventOfCode;

public class Program
{
    public static void Main(string[] args)
    {
        Solver.Solve<Day_03>(options => {
            options.ShowTotalElapsedTimePerDay = false;
            options.ShowOverallResults = false;
            options.ClearConsole = true;
            options.ElapsedTimeFormatSpecifier = "F3";
        }).Wait();
    }
}