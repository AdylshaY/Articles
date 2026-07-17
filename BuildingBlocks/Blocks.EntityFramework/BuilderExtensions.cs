namespace Blocks.EntityFramework
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
    using System.Text.Json;

    public static class BuilderExtensions
    {
        /// <summary>
        /// Configures the property to store enum values as their string representation in the database.
        /// </summary>
        /// <typeparam name="TEnum">The enum type of the property.</typeparam>
        /// <param name="builder">The property builder being configured.</param>
        /// <returns>The same property builder instance for chaining.</returns>
        public static PropertyBuilder<TEnum> HasEnumConversion<TEnum>(this PropertyBuilder<TEnum> builder) where TEnum : Enum
        {
            builder.HasConversion(
                    v => v.ToString(),
                    v => (TEnum)Enum.Parse(typeof(TEnum), v)
                );
            return builder;
        }

        /// <summary>
        /// Configures the property to store a collection as JSON in the database.
        /// </summary>
        /// <typeparam name="T">The collection type to be stored as JSON.</typeparam>
        /// <param name="builder">The property builder to configure.</param>
        /// <returns>The same property builder instance for method chaining.</returns>
        public static PropertyBuilder<T> HasJsonCollectionConversion<T>(this PropertyBuilder<T> builder) where T : class
        {
            return builder.HasConversion(BuildJsonListConvertor<T>());
        }

        /// <summary>
        /// Builds a value converter that serializes and deserializes a collection to and from JSON.
        /// </summary>
        /// <remarks>Null string values are deserialized as an empty JSON array.</remarks>
        /// <typeparam name="TCollection">The collection type to convert.</typeparam>
        /// <returns>A value converter that converts between the collection type and its JSON string representation.</returns>
        public static ValueConverter<TCollection, string> BuildJsonListConvertor<TCollection>()
        {
            Func<TCollection, string> serializeFunc = v => JsonSerializer.Serialize(v);
            Func<string, TCollection> deserializeFunc = v => JsonSerializer.Deserialize<TCollection>(v ?? "[]")!;

            return new ValueConverter<TCollection, string>(v => serializeFunc(v), v => deserializeFunc(v));
        }
    }
}
