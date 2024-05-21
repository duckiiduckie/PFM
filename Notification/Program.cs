using MassTransit;
using Notification.Models;
using Notification.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", h =>
        {
            h.Username("duckie");
            h.Password("01");
        });
        cfg.ConfigureEndpoints(context);
    });
    x.AddConsumer<NotiConsumer>();
});

builder.Services.AddScoped<NotiConsumer>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
