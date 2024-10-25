namespace hotel_search.api
{
    public static class ProgramExtensions
    {
        public static WebApplicationBuilder? AddHotelSearchRepository(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton(typeof(IHotelSearchRepository), serviceProvider => {
                var config = serviceProvider.GetService<IConfiguration>();
                var connectionString = config!.GetConnectionString("Primary");
                return new HotelSqliteDatabase(connectionString!);
            });
            return builder;
        }
    }
}
