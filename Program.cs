using AoCHelper;
using Formula9.AdventOfCode.Solutions2023.Day01;
using Formula9.AdventOfCode.Solutions2023.Day02;

namespace Formula9.AdventOfCode;

public class Program
{
    public static void Main(string[] args)
    {
        Solver.Solve<Day_02>(options => {
            options.ShowTotalElapsedTimePerDay = false;
            options.ShowOverallResults = false;
            options.ClearConsole = true;
            options.ElapsedTimeFormatSpecifier = "F3";
        }).Wait();
    }
}