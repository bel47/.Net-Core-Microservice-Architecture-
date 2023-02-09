using Microsoft.EntityFrameworkCore;
using ProductWebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Database Context Dependency Injection
var dHost = Environment.GetEnvironmentVariable("DB_HOST");
var dName = Environment.GetEnvironmentVariable("DB_Name");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
var connectionString = $"server={dHost};port=3306;database={dName};user=root;password={dbPassword}";
builder.Services.AddDbContext<ProductDbContext>(opt => opt.UseMySQL(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Bel-Note: uncomment to enable https
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
