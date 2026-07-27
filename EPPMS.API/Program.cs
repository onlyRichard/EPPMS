using EPPMS.API.Extensions;
using EPPMS.Application.DependencyInjection;
using EPPMS.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

#region Services

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

#endregion

var app = builder.Build();

#region Middleware

app.UseGlobalExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#endregion

app.Run();