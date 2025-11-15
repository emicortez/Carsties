using AuctionService;
using AuctionService.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

builder.Services.AddControllers();
builder.Services.AddDbContext<AuctionDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
});

builder.Services.AddScoped<IAuctionRepository, AuctionRepository>();

builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

try
{
    DbInitializer.InitDb(app);
}
catch (Exception ex)
{

    Console.WriteLine($"An error occurred while initializing the database: {ex.Message}");
}

app.Run();


