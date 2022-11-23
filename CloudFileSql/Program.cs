using CloudFileSql.Data;
using CloudFileSql.Domain;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connectionString = builder.Configuration["DatabaseOptions:ConnectionStrings:SQL"];

builder.Services.AddDbContext<StorageDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<IFileRepo, FileRepo>();
builder.Services.AddScoped<IFileManager , FileManager >();

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
