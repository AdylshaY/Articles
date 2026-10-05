namespace Journals.Domain.Journals
{
    using Blocks.Redis;
    using Journals.ValueObjects;
    using Redis.OM.Modeling;

    [Document(StorageType = StorageType.Json)]
    public class Section : Entity
    {
        [Indexed]
        public required string Name { get; set; }
        public required string Description { get; set; }
        public List<SectionEditor> EditorRoles { get; set; } = new();
    }
}
