using AoCHelper;
using Formula9.AdventOfCode.Solutions2023.Day10;

namespace Formula9.AdventOfCode;

public class Program
{
    public static void Main(string[] args)
    {
        Type day = typeof(Day_10);
        // var _d = Activator.CreateInstance(day) as AdventOfCodeProblem;
        // Console.WriteLine(_d.Solve_1().AsTask().Result);
        // Console.WriteLine(_d.Solve_2().AsTask().Result);
        Solver.Solve(options => {
            options.ShowTotalElapsedTimePerDay = false;
            options.ShowOverallResults = false;
            options.ClearConsole = true;
            options.ElapsedTimeFormatSpecifier = "F3";
        }, day).Wait();
    }
}