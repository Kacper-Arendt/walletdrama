using Shared.Abstractions.ValueObjects;

namespace Shared.Domain.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be empty.", nameof(value));
        
        if (value.Length > 100)
            throw new ArgumentException("Email cannot exceed 100 characters.", nameof(value));

        Value = value;
    }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();

    public static implicit operator string(Email userId) => userId.Value;
    public static explicit operator Email(string value) => new(value);
}