namespace Blocks.Core
{
    public static class Guard
    {
        public static void ThrowIfNullOrWhitespace(string value) => ArgumentException.ThrowIfNullOrWhiteSpace(value);
    }
}
