namespace Articles.Abstractions
{
    using Blocks.Domain;

    public interface IArticleAction : IAuditableAction
    {
        public int ArticleId { get; }
    }

    public interface IArticleAction<TActionType> : IArticleAction, IAuditableAction<TActionType>
        where TActionType : Enum
    {

    }
}
