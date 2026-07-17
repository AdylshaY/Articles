namespace Blocks.MediatR.Behaviors
{
    using Blocks.Domain;
    using global::MediatR;

    public class SetUserIdBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IAuditableAction
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            request.CreatedById = 1; //TODO: Get the user id from the current user context or authentication service

            return await next(cancellationToken);
        }
    }
}
