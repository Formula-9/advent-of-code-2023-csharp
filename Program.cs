using AoCHelper;
using Formula9.AdventOfCode.Solutions2023.Day06;
using Formula9.AdventOfCode.Solutions2023.Day07;

namespace Formula9.AdventOfCode;

public class Program
{
    public static void Main(string[] args)
    {
        Solver.Solve<Day_07>(options => {
            options.ShowTotalElapsedTimePerDay = false;
            options.ShowOverallResults = false;
            options.ClearConsole = true;
            options.ElapsedTimeFormatSpecifier = "F3";
        }).Wait();
        // Day_07 d = new();
        // var x = d.Solve_1().AsTask().Result;
        // Console.WriteLine(x);
        // var y = d.Solve_2().AsTask().Result;
        // Console.WriteLine(y);
    }
}