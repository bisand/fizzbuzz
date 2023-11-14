namespace FizzBuzz.Numbers;

public static class EnumerableExtensions
{
    /// <summary>
    /// Enumerates all values between the first and second value in range. 
    /// Automatically handles the enumeration-direction.
    /// </summary>
    /// <param name="range">The first parameter specifies the first value of the enumeration, 
    /// the second parameter specifies the last value of the enumeration.</param>
    public static IEnumerable<int> EnumerateRange(this (int from, int to) range)
    {
        return (range.from <= range.to)
                ? Enumerable.Range(range.from, (range.to - range.from) + 1)
                : Enumerable.Range(range.to, (range.from - range.to) + 1).Reverse();
    }

    /// <summary>
    /// ForEach support for IEnumerable
    /// </summary>
    /// <param name="enumeration">The enumeration</param>
    /// <param name="action">The action to call</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
    {
        foreach (var item in enumeration) action(item);
        return enumeration;
    }
}