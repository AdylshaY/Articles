namespace Auth.API.Features.Login;

using Auth.Application;
using Auth.Domain.Users;
using Blocks.AspNetCore.Extensions;
using Blocks.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

[AllowAnonymous]
[HttpPost("login")]
public class LoginEndpoint(UserManager<User> userManager, SignInManager<User> signInManager, TokenFactory tokenFactory)
    : Endpoint<LoginCommand, LoginResponse>
{
    public override async Task HandleAsync(LoginCommand command, CancellationToken ct)
    {
        var user = await userManager.FindByEmailAsync(command.Email) ?? throw new BadRequestException($"User {command.Email} not found");

        var result = await signInManager.CheckPasswordSignInAsync(user, command.Password, false);
        if (!result.Succeeded)
            throw new BadRequestException($"Invalid username or password");

        var userRoles = await userManager.GetRolesAsync(user);

        var jwtToken = tokenFactory.GenerateJwtToken(user.Id.ToString(), user.FullName, command.Email, userRoles, Array.Empty<Claim>());
        var refreshToken = tokenFactory.GenerateRefreshToken(HttpContext.GetClientIpAddress());
        user.AddRefreshToken(refreshToken);
        await userManager.UpdateAsync(user);

        await Send.OkAsync(new LoginResponse(command.Email, jwtToken, refreshToken.Token), ct);
    }
}