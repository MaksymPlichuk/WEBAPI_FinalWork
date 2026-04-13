using Microsoft.EntityFrameworkCore;
using WEBAPI_FinalWork.DAL;
using WEBAPI_FinalWork.DAL.Initilizer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    string? connectionString = builder.Configuration.GetConnectionString("LocalDB");
    opt.UseNpgsql(connectionString);
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
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
app.Seed().Wait();

app.Run();
