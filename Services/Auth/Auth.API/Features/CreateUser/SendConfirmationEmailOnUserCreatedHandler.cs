namespace Auth.API.Features.CreateUser
{
    using Auth.Domain.Users;
    using Auth.Domain.Users.Events;
    using Blocks.AspNetCore.Extensions;
    using EmailService.Contracts;
    using FastEndpoints;
    using Flurl;
    using Microsoft.Extensions.Options;

    public class SendConfirmationEmailOnUserCreatedHandler(IEmailService emailService, IOptions<EmailOptions> emailOptions, IHttpContextAccessor httpContextAccessor) : IEventHandler<UserCreated>
    {
        public async Task HandleAsync(UserCreated eventModel, CancellationToken ct)
        {
            var url = httpContextAccessor.HttpContext?.Request.BaseUrl().AppendPathSegment("password").SetQueryParams(new { eventModel.ResetPasswordToken });
            var emailMessage = BuildConfirmationEmail(eventModel.user, url, emailOptions.Value.EmailFromAddress);
            await emailService.SendEmailAsync(emailMessage, ct);
        }

        public EmailMessage BuildConfirmationEmail(User user, string resetLink, string fromEmailAddress)
        {
            const string ConfirmationEmail = "Dear {0},<br/>An account has been created for you.<br/>Please set your password using the following link:<br/>{1}<br/>Thank you.";

            return new EmailMessage(
                "Your Account Has Been Created - Set Your Password",
                new Content(ContentType.Html, string.Format(ConfirmationEmail, user.FullName, resetLink)),
                new EmailAddress("Articles", fromEmailAddress),
                [new(user.FullName, user.Email!)]
            );
        }
    }
}
