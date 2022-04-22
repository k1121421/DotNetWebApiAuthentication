using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using WebApiUsingJWT;
using WebApiUsingJWT.Authorization;
using WebApiUsingJWT.Middleware;
using WebApiUsingJWT.Models;
using WebApiUsingJWT.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AuthDbContext>();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSwaggerGen();

builder.Services.AddApiVersioning(config =>
{
    // Specify the default API Version
    config.DefaultApiVersion = new ApiVersion(1, 0);
    // If the client hasn't specified the API version in the request, use the default API version number 
    config.AssumeDefaultVersionWhenUnspecified = true;
    // Advertise the API versions supported for the particular endpoint
    config.ReportApiVersions = true;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
    var testUser = new User
    {
        Username = "test",
        PasswordHash = BCrypt.Net.BCrypt.HashPassword("test")
    };
    context.Users.Add(testUser);
    context.SaveChanges();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
