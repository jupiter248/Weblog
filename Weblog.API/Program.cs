
using System.Text.Json.Serialization;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Weblog.API.Middleware;
using Weblog.Application;
using Weblog.Application.Extensions;
using Weblog.Infrastructure.Extension;
using Weblog.Persistence.Data;
using Weblog.Persistence.Extensions;

Env.Load(Path.Combine("..", ".env")); // This loads .env into Environment variables

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwaggerGen();

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).Assembly));

builder.Services.AddControllers();

builder.Services.ApplyDependencies(); // Apply dependencies like repository and service scopes 

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

app.UseMiddleware<ErrorHandlingMiddleware>();

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
