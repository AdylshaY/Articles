namespace Submission.Application.Features.UploadFile.UploadManuscriptFile
{
    using Blocks.EntityFramework;
    using System.Threading.Tasks;

    public class UploadManuscriptFileCommandHandler(ArticleRepository _articleRepository) : IRequestHandler<UploadManuscriptFileCommand, IdResponse>
    {
        public async Task<IdResponse> Handle(UploadManuscriptFileCommand command, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetByIdOrThrowAsync(command.ArticleId);
            return new IdResponse(0);
        }
    }
}
