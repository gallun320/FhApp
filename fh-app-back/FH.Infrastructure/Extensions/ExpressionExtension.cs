using System.Linq.Expressions;
using System.Reflection;

namespace FH.Infrastructure.Extensions;

/// <summary>
/// Extentions from expression
/// </summary>
public static class ExpressionExtension
{
    /// <summary>
    /// Return <see cref="PropertyInfo"/> from expression.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="exp"></param>
    /// <returns></returns>
    public static PropertyInfo? GetPropertyInfo<T>(this Expression<Func<T, object>> exp)
    {
        return typeof(T).GetProperty(exp.GetFullPropertyName());
    }

    /// <summary>
    /// Return full property name.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="exp"></param>
    /// <returns></returns>
    public static string GetFullPropertyName<T>(this Expression<Func<T, object>> exp)
    {
        if (!TryFindMemberExpression(exp.Body, out var memberExp))
        {
            return string.Empty;
        }

        var memberNames = new Stack<string>();
        do
        {
            memberNames.Push(memberExp!.Member.Name);
        }
        while (TryFindMemberExpression(memberExp.Expression, out memberExp));

        return string.Join(".", memberNames.ToArray());
    }

    // code adjusted to prevent horizontal overflow
    private static bool TryFindMemberExpression(Expression? exp, out MemberExpression? memberExp)
    {
        memberExp = exp as MemberExpression;
        if (memberExp != null)
        {
            // heyo! that was easy enough
            return true;
        }

        // if the compiler created an automatic conversion,
        // it'll look something like...
        // obj => Convert(obj.Property) [e.g., int -> object]
        // OR:
        // obj => ConvertChecked(obj.Property) [e.g., int -> long]
        // ...which are the cases checked in IsConversion
        if (exp.IsConversion()
            && exp is UnaryExpression ue
            && ue.Operand is MemberExpression value)
        {
            memberExp = value;
            return true;
        }

        return false;
    }
    
    /// <summary>
    /// Return list of attrs that point type
    /// </summary>
    /// <typeparam name="T">The type of the custom attributes.</typeparam>
    /// <param name="attributeProvider">Provides custom attributes for reflection objects that support them.</param>
    /// <param name="inherit">When true, look up the hierarchy chain for the inherited custom attribute.</param>
    /// <returns><see cref="IEnumerable{T}"/> representing custom attributes.</returns>
    public static IEnumerable<T> GetAttributes<T>(
        this ICustomAttributeProvider attributeProvider,
        bool inherit = true)
    {
        return attributeProvider.GetCustomAttributes(typeof(T), inherit).OfType<T>();
    }

    /// <summary>
    /// Return attr value 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="attributeProvider"></param>
    /// <param name="property"></param>
    /// <param name="inherit"></param>
    /// <returns></returns>
    public static TValue? GetAttributeValue<T, TValue>(
        this ICustomAttributeProvider attributeProvider,
        Func<T, TValue> property,
        bool inherit = true)
    {
        var a = attributeProvider.GetAttributes<T>(inherit).FirstOrDefault();
        if (a == null)
            return default;
        return property(a);
    }

    /// <summary>
    /// Return enumerable names of type properties
    /// </summary>
    /// <param name="type">Type</param>
    /// <returns>Enumerable names</returns>
    public static IEnumerable<string> GetPropertyNames(this Type type)
    {
        return type.GetProperties().Select(p => p.Name);
    }

    /// <summary>
    /// Return inherits type
    /// </summary>
    /// <typeparam name="T">Base type</typeparam>
    /// <param name="baseType"></param>
    /// <param name="includeAbstract">Turn abstract</param>
    /// <returns></returns>
    public static IEnumerable<Type> GetInherits(this Type baseType, bool includeAbstract = false)
    {
        return baseType.Assembly.GetTypes()
            .Where(t => baseType.IsAssignableFrom(t))
            .Where(t => !t.IsAbstract || includeAbstract);
    }

    /// <summary>
    /// Return type for <see cref="Nullable"/> value.
    /// If type is not <see cref="Nullable"/>, return type.
    /// </summary>
    /// <param name="type"></param>
    public static Type GetNullableUnderlyingType(this Type type)
    {
        return Nullable.GetUnderlyingType(type) ?? type;
    }
    
    private static bool IsConversion(this Expression exp) => exp.NodeType.IsIn(ExpressionType.Convert, ExpressionType.ConvertChecked);
}
