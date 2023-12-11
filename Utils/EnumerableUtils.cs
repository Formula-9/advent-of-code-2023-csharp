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

    public static T Next<T>(this IEnumerator<T> enumerator)
    {
        enumerator.MoveNext();
        return enumerator.Current;
    }

    public static double Product<T>(this IEnumerable<T> values, Func<T, double> keySelector) => values.Aggregate(1d, (acc, el) => acc * keySelector(el));

    public static IEnumerable<(int, T)> Enumerate<T>(this IEnumerable<T> values, int start = 0)
    {
        foreach (T element in values)
        {
            yield return (start, element);
            start++;
        }
        yield break;
    }

    public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> values, int k)
    {
        return k == 0 
        ? EnumerableEx.Return(Enumerable.Empty<T>()) 
        : values.SelectMany((e, i) => 
            values.Skip(i + 1)
                .Combinations(k - 1)
                .Select(c => EnumerableEx.Return(e).Concat(c)));
    }
}