using Submission.API;
using Submission.API.Endpoints;
using Submission.Application;
using Submission.Persistence;

var builder = WebApplication.CreateBuilder(args);

#region Add Services

builder.Services
    .AddApiServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration
    ).AddPersistenceServices(builder.Configuration);

#endregion

var app = builder.Build();

#region Use Services

app.UseSwagger()
   .UseSwaggerUI()
   .UseRouting();

app.MapAllEndpoints();

//TODO: Migrate - Create first migration

if(app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //TODO: Set up database seeding for development environment
}

#endregion


app.Run();
