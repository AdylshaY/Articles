namespace Submission.Application.Features.CreateAndAssignAuthor
{
    using Blocks.EntityFramework;
    using Submission.Persistence;
    using System.Threading.Tasks;


    public class CreateAndAssignAuthorCommandHandler(ArticleRepository _articleRepository)
        : IRequestHandler<CreateAndAssignAuthorCommand, IdResponse>
    {
        public async Task<IdResponse> Handle(CreateAndAssignAuthorCommand command, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetByIdOrThrowAsync(command.ArticleId);

            Author? author = null;
            if (command.UserId == null)
            {
                author = Author.Create(command.Email!, command.FirstName!, command.LastName!, command.Title!, command.Affiliation!);
            }
            else
            {
                //TODO: Author is an User
            }

            article.AssignAuthor(author, command.ContributionAreas, command.IsCorrespondingAuthor);

            await _articleRepository.SaveChangesAsync(cancellationToken);

            return new IdResponse(article.Id);
        }
    }
}
