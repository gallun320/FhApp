namespace FH.Infrastructure.Extensions;

/// <summary>
/// Extensions collection class
/// </summary>
public static class CollectionExtension
{
    /// <summary>
    /// Do action with each piece of collection 
    /// </summary>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T>? action)
    {
        if (action == null)
            return;
        
        foreach (var i in source)
            action(i);
    }
    
    /// <summary>
    /// Do action with each piece of collection 
    /// </summary>
    public static async Task ForEachAsync<T>(this IEnumerable<T> source, Func<T, Task>? action)
    {
        if (action == null)
            return;
        
        foreach (var i in source)
            await action(i);
    }
    
    /// <summary>
    /// Do action with each piece of collection if condition true
    /// </summary>
    public static void ForEachIf<T>(this IEnumerable<T> source, bool condition, Action<T>? action)
    {
        if(!condition)
            return;
        
        if (action == null)
            return;
        
        foreach (var i in source)
            action(i);
    }
    
    /// <summary>
    /// Return <see cref="true"/> if collection empty , or <see cref="false"/>.
    /// </summary>
    /// <typeparam name="T">type elements in collection.</typeparam>
    /// <param name="source">Collection.</param>
    /// <returns></returns>
    public static bool IsNullOrEmpty<T>(this ICollection<T>? source)
    {
        return source == null || !source.Any();
    }

    /// <summary>
    /// Return <see cref="true"/> if it collection.
    /// </summary>
    /// <typeparam name="T">Type of value.</typeparam>
    /// <param name="value">Value.</param>
    /// <param name="source">Collection.</param>
    /// <returns></returns>
    public static bool IsIn<T>(this T? value, params T[]? source)
    {
        return !source.IsNullOrEmpty() && source!.Contains(value);
    }

    /// <summary>
    /// Break collection on groups by <paramref name="groupSize"/>.
    /// </summary>
    /// <typeparam name="T">Type of elements.</typeparam>
    /// <param name="source">Source.</param>
    /// <param name="groupSize">Size.</param>
    /// <returns></returns>
    public static IEnumerable<IEnumerable<T>> GetGroups<T>(this IEnumerable<T> source, int groupSize)
    {
        return source
            .Select((x, index) => new { x, index })
            .GroupBy(x => x.index / groupSize)
            .Select(g => g.Take(groupSize).Select(x => x.x));
    }
}