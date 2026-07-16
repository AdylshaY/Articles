namespace Blocks.EntityFramework
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public static class BuilderExtensions
    {
        public static PropertyBuilder<TEnum> HasEnumConversion<TEnum>(this PropertyBuilder<TEnum> builder) where TEnum : Enum
        {
            builder.HasConversion(
                    v => v.ToString(),
                    v => (TEnum)Enum.Parse(typeof(TEnum), v)
                );
            return builder;
        }
    }
}
