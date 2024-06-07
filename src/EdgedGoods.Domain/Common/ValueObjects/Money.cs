using System.Numerics;

namespace EdgedGoods.Domain.Common.ValueObjects;

public readonly record struct Money(decimal Value, string Currency) :
    IComparisonOperators<Money, Money, bool>,
    IComparable<Money>
{
 
    /// <summary>
    /// Overloads the addition operator to add two Money objects and returns a new Money object with the sum of the values.
    /// </summary>
    /// <param name="left">The left Money object to add.</param>
    /// <param name="right">The right Money object to add.</param>
    /// <returns>A new Money object with the sum of the values.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the currencies are not equal.</exception>
    public static Money operator +(Money left, Money right)
    {
        ThrowIfCurrencyNotEqual(left, right);
        return left with { Value = left.Value + right.Value };
    }
    
    /// <summary>
    /// Overloads the subtraction operator to subtract two Money objects and returns a new Money object with the result.
    /// </summary>
    /// <param name="left">The left Money object to subtract from.</param>
    /// <param name="right">The right Money object to subtract.</param>
    /// <exception cref="InvalidOperationException">Thrown if the currencies are not equal.</exception>
    /// <exception cref="ArgumentException">Thrown if the left Money object is less than the right Money object.</exception>
    /// <returns>A new Money object with the result of the subtraction.</returns>
    public static Money operator -(Money left, Money right)
    {
        ThrowIfCurrencyNotEqual(left, right);
        if (left.Value < right.Value) throw new ArgumentException("Left money object less then right");
        return left with { Value = left.Value - right.Value };
    }
    
    /// <summary>
    /// Multiplies the value of a Money object by a scalar.
    /// </summary>
    /// <param name="money">The Money object to be multiplied.</param>
    /// <param name="scalar">The scalar value to multiply the Money object by.</param>
    /// <returns>A new Money object with the multiplied value.</returns>
    public static Money operator *(Money money, int scalar) => money with { Value = money.Value * scalar };
    
    /// <summary>
    /// Multiplies the value of a Money object by a scalar.
    /// </summary>
    /// <param name="money">The Money object to be multiplied.</param>
    /// <param name="scalar">The scalar value to multiply the Money object by.</param>
    /// <returns>A new Money object with the multiplied value.</returns>
    public static Money operator *(int scalar, Money money) => money * scalar;
    
    /// <summary>
    /// Divides the value of a Money object by a scalar.
    /// </summary>
    /// <param name="money">The Money object to be divided.</param>
    /// <param name="scalar">The scalar value to divide the Money object by.</param>
    /// <returns>A new Money object with the divided value.</returns>
    public static Money operator /(Money money, int scalar) => money with { Value = money.Value / scalar };

    /// <summary>
    /// Overloads the greater than operator to compare two Money objects and returns a boolean indicating whether the left Money object is greater than the right Money object.
    /// </summary>
    /// <param name="left">The left Money object to compare.</param>
    /// <param name="right">The right Money object to compare.</param>
    /// <exception cref="InvalidOperationException">Thrown if the currencies are not equal.</exception>
    /// <returns>True if the left Money object is greater than the right Money object, false otherwise.</returns>
    public static bool operator >(Money left, Money right)
    {
        ThrowIfCurrencyNotEqual(left, right);
            
        return left.Value > right.Value;
    }

    /// <summary>
    /// Overloads the greater or equal than operator to compare two Money objects and returns a boolean indicating whether the left Money object is greater or equal than the right Money object.
    /// </summary>
    /// <param name="left">The left Money object to compare.</param>
    /// <param name="right">The right Money object to compare.</param>
    /// <exception cref="InvalidOperationException">Thrown if the currencies are not equal.</exception>
    /// <returns>True if the left Money object is greater or equal than the right Money object, false otherwise.</returns>
    public static bool operator >=(Money left, Money right)
    {
        ThrowIfCurrencyNotEqual(left, right);
        
        return left.Value >= right.Value;
    }

    /// <summary>
    /// Overloads the less than operator to compare two Money objects and returns a boolean indicating whether the left Money object is less than the right Money object.
    /// </summary>
    /// <param name="left">The left Money object to compare.</param>
    /// <param name="right">The right Money object to compare.</param>
    /// <exception cref="InvalidOperationException">Thrown if the currencies are not equal.</exception>
    /// <returns>True if the left Money object is less than the right Money object, false otherwise.</returns>
    public static bool operator <(Money left, Money right)
    {
        ThrowIfCurrencyNotEqual(left, right);
        
        return left.Value < right.Value;
    }

    /// <summary>
    /// Overloads the less or equal than operator to compare two Money objects and returns a boolean indicating whether the left Money object is less or equal than the right Money object.
    /// </summary>
    /// <param name="left">The left Money object to compare.</param>
    /// <param name="right">The right Money object to compare.</param>
    /// <exception cref="InvalidOperationException">Thrown if the currencies are not equal.</exception>
    /// <returns>True if the left Money object is less or equal than the right Money object, false otherwise.</returns>
    public static bool operator <=(Money left, Money right)
    {
        ThrowIfCurrencyNotEqual(left, right);
        
        return left.Value <= right.Value;
    }
    
    /// <summary>
    /// Throws an exception if the currencies of two Money objects are not equal.
    /// </summary>
    /// <param name="left">The first Money object.</param>
    /// <param name="right">The second Money object.</param>
    /// <exception cref="InvalidOperationException">Thrown if the currencies are not equal.</exception>
    private static void ThrowIfCurrencyNotEqual(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new InvalidOperationException("Can't compare money value of different currency");
    }
    
    /// <summary>
    /// Throws an exception if the currencies of this Money object not equal to currency of other Money object.
    /// </summary>
    /// <param name="other">The other Money object.</param>
    /// <exception cref="InvalidOperationException">Thrown if the currencies are not equal.</exception>
    private void ThrowIfCurrencyNotEqual(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException("Can't compare money value of different currency");
    }

    public int CompareTo(Money other)
    {
        ThrowIfCurrencyNotEqual(other);
        return Value < other.Value ? -1
            : Value == other.Value ? 0 
                : -1;
    }

    public override string ToString() => $"{Value} {Currency}";
}

public static class MoneyExtensions
{
    public static Money Sum(this IEnumerable<Money> monies)
    {
        var enumerable = monies as Money[] ?? monies.ToArray();
        return new Money(enumerable.Sum(m => m.Value), enumerable.First().Currency);
    }
}