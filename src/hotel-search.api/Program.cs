using hotel_search.api;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(typeof(IHotelSearchRepository), serviceProvider => {
    var config = serviceProvider.GetService<IConfiguration>();
    var connectionString = config!.GetConnectionString("Primary");
    return new HotelSqliteDatabase(connectionString!);
});
builder.Services.AddScoped<HotelSearchService>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();



