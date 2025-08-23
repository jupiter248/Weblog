
using System.IO;
using System.Text.Json.Serialization;
using DotNetEnv;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Weblog.API.Middleware;
using Weblog.Application;
using Weblog.Application.Extensions;
using Weblog.Infrastructure.Extension;
using Weblog.Persistence.Data;
using Weblog.Persistence.Extensions;

Env.Load(Path.Combine("..", ".env")); // This loads .env into Environment variables

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

// var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
// Console.WriteLine("👉 Connection String: " + connectionString);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwaggerGen();

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        // This disables the default automatic 400 response
        options.SuppressModelStateInvalidFilter = true;
    });

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).Assembly));

builder.Services.AddControllers();

builder.Services.AddInfrastructure(); // Apply dependencies like repository and service scopes 
builder.Services.AddPersistence();

builder.Services.ConnectToDatabase();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJwt();


builder.Services.ApplyAutoMapper();


builder.Services.AddCors(opt =>
{
    opt.AddPolicy("HelloConnection", policy =>
    {
        policy.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin();
    });
});
builder.Services.AddHttpClient();

var app = builder.Build();

var supportedCultures = new[] { "en", "fa" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("en")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);
app.UseMiddleware<ErrorHandlerMiddleware>();

await app.Services.ApplyMigrations();

app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();


app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors("HelloConnection");

app.MapControllers();

app.Run();
