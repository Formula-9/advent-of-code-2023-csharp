using AoCHelper;
using Formula9.AdventOfCode.Solutions2023.Day01;

namespace Formula9.AdventOfCode;

public class Program
{
    public static void Main(string[] args)
    {
        Solver.Solve<Day_01>(options => {
            options.ShowTotalElapsedTimePerDay = false;
            options.ShowOverallResults = false;
            options.ClearConsole = true;
            options.ElapsedTimeFormatSpecifier = "F3";
        }).Wait();
    }
}