using hotel_search.api;

var builder =
    WebApplication
    .CreateBuilder(args)
    .AddJwtAuthentication()
    .AddHotelSearchRepository(seedTestHotelData: true);

builder!.Services.AddScoped<HotelSearchService>();
builder.Services.AddHealthChecks();
builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health-check");
app.Run();