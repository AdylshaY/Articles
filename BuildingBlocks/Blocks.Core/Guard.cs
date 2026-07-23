namespace Blocks.Core
{
    public static class Guard
    {
        public static void ThrowIfNullOrWhitespace(string value) => ArgumentException.ThrowIfNullOrWhiteSpace(value);

        public static void ThrowIfNotEqual<T>(T value, T expected) where T : IEquatable<T>? => ArgumentOutOfRangeException.ThrowIfNotEqual(value, expected);
    }
}
