using Microsoft.EntityFrameworkCore;
using BE_MatOppskrift.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext with SQLite (modify for other databases)
builder.Services.AddDbContext<RecipeContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))); // Read connection string from appsettings.json

// Add CORS policy in ConfigureServices so that svelte frontend can access the c# backend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSvelteKitFrontend",
        builder => builder.WithOrigins("http://localhost:5173") // Replace with your SvelteKit dev server URL
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

// Use CORS middleware AFTER app.UseRouting() and BEFORE app.UseAuthorization()
app.UseRouting();
app.UseCors("AllowSvelteKitFrontend"); // Apply the CORS policy
app.UseAuthorization();
app.MapControllers();

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





