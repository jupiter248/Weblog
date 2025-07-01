
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

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).Assembly));


builder.Services.AddControllers();

builder.Services.ApplyDependencies(); // Apply dependencies like repository and service scopes 

builder.Services.ConnectToDatabase();

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

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate(); // Ensure the database is created and migrations are applied
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors("HelloConnection");

app.MapControllers();

app.Run();
