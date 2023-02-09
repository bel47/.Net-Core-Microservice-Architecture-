using JwtAutenticationManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddeCustomeJwtAuthentication();

var app = builder.Build();
await app.UseOcelot(); //this  method can be awaited

app.UseAuthentication();
app.UseAuthorization();
app.Run();
