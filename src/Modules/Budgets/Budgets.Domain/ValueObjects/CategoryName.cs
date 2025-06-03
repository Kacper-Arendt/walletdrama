using Shared.Abstractions.Exceptions;
using Shared.Abstractions.ValueObjects;

namespace Budgets.Domain.ValueObjects;

public class CategoryName : ValueObject
{
    public string Value { get; }

    public CategoryName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("CategoryName cannot be empty.", nameof(value));
        
        if (value.Length > 50)
            throw new ValueObjectInvalidTypeException("CategoryName cannot exceed 50 characters.");

        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(CategoryName name) => name.Value;
    public static explicit operator CategoryName(string value) => new(value);
    
}