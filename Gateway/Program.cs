using Gateway.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(o => o.AddPolicy("AllowAnyOrigin", builder =>
{
    builder.WithOrigins("*").AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));
builder.AddAppAuthetication();
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelotDev.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

app.UseCors("AllowAnyOrigin");
app.UseOcelot().GetAwaiter().GetResult();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
