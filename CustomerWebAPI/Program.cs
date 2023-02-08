using CustomerWebAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Database Context Dependency Injection
/*var dHost = "localhost";
var dName = "dms_customer";
var dbPassword = "P@ssw0rd";*/
var dHost = Environment.GetEnvironmentVariable("DB_HOST");
var dName = Environment.GetEnvironmentVariable("DB_Name");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
//var connectionString = $"Data Source={dHost},1433;Initial Catalog={dName};User ID=sa;Password={dbPassword};TrustServerCertificate=True";
var connectionString = $"Data Source={dHost};Initial Catalog={dName};User ID=sa;Password={dbPassword};TrustServerCertificate=True";

builder.Services.AddDbContext<CustomerDbContext>(opt => opt.UseSqlServer(connectionString));
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
