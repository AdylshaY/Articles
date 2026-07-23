namespace Blocks.Domain.ValueObjects
{
    using System;
    using System.Numerics;

    public abstract class StringValueObject : IEquatable<StringValueObject>, IEquatable<string>
    {
        public string Value { get; protected set; } = default!;

        public override string ToString() => Value.ToString();
        public override int GetHashCode() => Value.GetHashCode();

        public bool Equals(string? other) => Value.Equals(other);
        public bool Equals(StringValueObject? other) => Value.Equals(other?.Value);

        public static implicit operator string(StringValueObject @object) => @object.Value;
    }
}
