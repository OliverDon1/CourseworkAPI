using CourseworkAPI_PLEASEKILLME_.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var connectionstrings = "Data Source=socem1.uopnet.plymouth.ac.uk;Database=COMP2001_ODonnelly;User Id=ODonnelly; Password=YsqH802+; Encrypt =False ; TrustServerCertificate=True";

var dbHost = "socem1.uopnet.plymouth.ac.uk;Database=COMP2001_ODonnelly";
var dbUser = "ODonnelly";
var dbPassword = "YsqH802+";
var dbName = "COMP2001_ODonnelly";
var connectionstring = $"Data Source = {dbHost} ; Database = {dbName}; User ID = {dbUser}; Password = {dbPassword}";
builder.Services.AddDbContext<COMP2001_ODonnellyContext>(opt => opt.UseSqlServer(connectionstring));


//SqlConnectionStringBuilder builder1 = new SqlConnectionStringBuilder(connectionstring);
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
