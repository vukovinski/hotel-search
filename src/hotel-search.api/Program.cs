using hotel_search.api;

var builder = WebApplication
    .CreateBuilder(args)
    .AddHotelSearchRepository();

builder!.Services.AddScoped<HotelSearchService>();
builder.Services.AddHealthChecks();
builder.Services.AddControllers();

var app = builder.Build();

app.MapHealthChecks("/health-check");
app.MapControllers();
app.Run();