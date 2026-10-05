namespace Journals.API.Features.Journals.Create
{
    using FastEndpoints;
    using Articles.Abstractions;
    using Microsoft.AspNetCore.Authorization;
    using Articles.Abstractions.Enums;

    [Authorize(Roles = Role.EOF)]
    [HttpPost("journals")]
    [Tags("Journals")]
    public class CreateJournalEndpoint : Endpoint<CreateJournalCommand, IdResponse>
    {
        public async override Task HandleAsync(CreateJournalCommand req, CancellationToken ct)
        {

        }
    }
}
