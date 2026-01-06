using EcommerceLearn.Api.Extensions.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServer(builder.Configuration);

var app = builder.Build();

app.UseApplication();

app.Run();