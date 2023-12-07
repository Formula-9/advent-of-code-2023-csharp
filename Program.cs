using AoCHelper;
using Formula9.AdventOfCode.Solutions2023.Day07;
using Formula9.AdventOfCode.Utils;

namespace Formula9.AdventOfCode;

public class Program
{
    public static void Main(string[] args)
    {
        Type day = typeof(Day_07);
        #if DEBUG
            var _day = Activator.CreateInstance(day) as AdventOfCodeProblem;
            var x = _day.Solve_1().AsTask().Result;
            Console.WriteLine(x);
            var y = _day.Solve_2().AsTask().Result;
            Console.WriteLine(y);
        #else
            Solver.Solve(options => {
                options.ShowTotalElapsedTimePerDay = false;
                options.ShowOverallResults = false;
                options.ClearConsole = true;
                options.ElapsedTimeFormatSpecifier = "F3";
            }, day).Wait();
        #endif
    }
}