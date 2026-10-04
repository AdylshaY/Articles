using Auth.API;
using Auth.Application;
using Auth.Persistence;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApiOptions(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApiServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddPersistenceServices(builder.Configuration);

var app = builder.Build();

app.UseRouting();

app.UseFastEndpoints().UseSwaggerGen();

app.UseHttpsRedirection();

app.MapControllers();

app.UseAuthorization();

app.Run();
