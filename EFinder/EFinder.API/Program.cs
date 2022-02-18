using EFinder.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureServices();

// Configure the HTTP request pipeline.
builder.Build().Configure();