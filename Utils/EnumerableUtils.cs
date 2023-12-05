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
}