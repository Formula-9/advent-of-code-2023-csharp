using System.Numerics;

namespace Formula9.AdventOfCode.Utils;

public static class EnumerableUtils
{
    public static bool Any<T>(this IEnumerable<T> source, Func<T, bool> predicate, out T firstElement)
    {
        firstElement = default;
        foreach (T item in source)
        {
            if (predicate(item))
            {
                firstElement = item;
                return true;
            }
        }
        return false;
    }

    public static IEnumerable<T> Sort<T>(this IEnumerable<T> source, IComparer<T> comparer)
    {
        var lst = source.ToList();
        lst.Sort(comparer);
        return lst;
    }

    public static long Sum<T>(this IEnumerable<T> source, Func<T, int, long> func)
    {
        long result = 0;
        int count = 0;
        foreach (T item in source)
        {
            result += func(item, count);
            count++;
        }
        return result;
    }
}