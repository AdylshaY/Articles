namespace Submission.Domain.Entities
{
    using Articles.Abstractions.Enums;
    using Blocks.Domain;

    public partial class Article
    {
        public void AssignAuthor(Author author, HashSet<ContributionArea> contributionAreas, bool isCorrespondingAuthor)
        {
            var role = isCorrespondingAuthor ? UserRoleType.CORAUT : UserRoleType.AUT;

            if (Actors.Exists(a => a.PersonId == author.Id && a.Role == role)) throw new DomainException($"Author {author.EmailAddress} is already assigned to the article.");

            Actors.Add(new ArticleAuthor
            {
                Person = author,
                ContributionAreas = contributionAreas,
                Role = role
            });

            //TODO: Create domain event for author assignment
        }

        public Asset CreateAsset(AssetTypeDefinition type)
        {
            var assetCount = _assets.Count(x => x.Type == type.Id);
            if (type.MaxAssetCount > assetCount - 1) throw new DomainException($"The maximum number of files allowed for {type.Name} was already reached.");
            var asset = Asset.Create(this, type);
            _assets.Add(asset);
            return asset;
        }
    }
}
