namespace Submission.Application.Features.CreateArticle
{
    using Articles.Abstractions;
    using Articles.Abstractions.Enums;
    using FluentValidation;
    using MediatR;

    public record CreateArticleCommand(int JournalId, string Title, string Scope, ArticleType ArticleType) : IRequest<IdResponse>;

    public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
    {
        public CreateArticleCommandValidator()
        {
            RuleFor(x => x.JournalId).GreaterThan(0).WithMessage("Invalid journal id");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty").MaximumLength(256).WithMessage("Title cannot exceed 256 characters");
            RuleFor(x => x.Scope).NotEmpty().WithMessage("Scope cannot be empty").MaximumLength(1000).WithMessage("Scope cannot exceed 1000 characters");
        }
    }
}
