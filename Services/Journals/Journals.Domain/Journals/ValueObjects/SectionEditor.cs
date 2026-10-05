namespace Journals.Domain.Journals.ValueObjects
{
    using Journals.Enums;
    using Redis.OM.Modeling;

    [Document]
    public class SectionEditor
    {
        public int EditorId { get; init; }
        public EditorRole EditorRole { get; set; }
    }
}
