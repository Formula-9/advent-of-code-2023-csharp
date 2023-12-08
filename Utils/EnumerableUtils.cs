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

    public static IEnumerable<T> RepeatInfinitely<T>(this IEnumerable<T> source)
    {
        while (true)
        {
            foreach (T element in source)
            {
                yield return element;
            }
        }
    }

    public static int Move<T>(this IEnumerator<T> enumerator, int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            if (!enumerator.MoveNext()) return i;
        }
        return steps;
    }

    public static IEnumerator<T> Move<T>(this IEnumerator<T> enumerator, int steps, out int stepsMoved)
    {
        stepsMoved = enumerator.Move(steps);
        return enumerator;
    }

    public static T Next<T>(this IEnumerator<T> enumerator)
    {
        enumerator.MoveNext();
        return enumerator.Current;
    }
}