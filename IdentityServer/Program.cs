//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//var app = builder.Build();

//// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//       new WeatherForecast
//       (
//           DateTime.Now.AddDays(index),
//           Random.Shared.Next(-20, 55),
//           summaries[Random.Shared.Next(summaries.Length)]
//       ))
//        .ToArray();
//    return forecast;
//});

//app.Run();

//internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}

using IdentityServer;

var builder = WebApplication.CreateBuilder(args);

//builder.Host.UseSerilog((ctx, lc) => lc
//    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
//    .Enrich.FromLogContext()
//    .ReadFrom.Configuration(ctx.Configuration));

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

app.Run();