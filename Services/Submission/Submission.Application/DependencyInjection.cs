namespace Submission.Application
{
    using Blocks.MediatR.Behaviors;
    using FluentValidation;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Submission.Application.Features.CreateArticle;
    using System.Reflection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddValidatorsFromAssemblyContaining<CreateArticleCommandValidator>()
                .AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                    cfg.AddOpenBehavior(typeof(SetUserIdBehavior<,>));
                    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                });

            return services;
        }
    }
}
