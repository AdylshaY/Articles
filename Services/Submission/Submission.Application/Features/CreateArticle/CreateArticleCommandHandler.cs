namespace Submission.Application.Features.CreateArticle
{
    using Articles.Abstractions;
    using MediatR;
    using Submission.Domain.Entities;
    using Submission.Persistence.Repositories;

    internal class CreateArticleCommandHandler(Repository<Journal> _journalRepository) : IRequestHandler<CreateArticleCommand, IdResponse>
    {
        public async Task<IdResponse> Handle(CreateArticleCommand command, CancellationToken cancellationToken)
        {
            var journal = await _journalRepository.FindByIdAsync(command.JournalId);
            // TODO: throw NotFoundException if journal is not found

            var article = journal.CreateArticle(command.Title, command.ArticleType, command.Scope);
            await _journalRepository.SaveChangesAsync(cancellationToken);

            return new IdResponse(article.Id);
        }
    }
}
