using Microsoft.EntityFrameworkCore;
using MoviesAPI.Models;
using MoviesAPI.MoviesMappers;
using MoviesAPI.Repository;
using MoviesAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MoviesAPIDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Services
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MoviesMapper));

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
