
namespace hotel_search.api
{
    public static class ProgramExtensions
    {
        public static WebApplicationBuilder? AddHotelSearchRepository(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton(typeof(IHotelSearchRepository), serviceProvider => {
                var config = serviceProvider.GetService<IConfiguration>();
                var connectionString = config!.GetConnectionString("Primary");
                var repository = new HotelSqliteDatabase(connectionString!);
                repository.CreateTables();
                SeedTables(repository);
                return repository;
            });
            return builder;
        }

        private static void SeedTables(HotelSqliteDatabase repository)
        {
            repository.InsertHotel(new Hotel { Id = 1, Name = "Hotel Zagreb", LocationLatitude = "45.8", LocationLongitude = "15.97", Price = 55.0m });
            repository.InsertHotel(new Hotel { Id = 2, Name = "Hotel Split", LocationLatitude = "43.51", LocationLongitude = "16.44", Price = 70.0m });
            repository.InsertHotel(new Hotel { Id = 3, Name = "Hotel Dubrovnik", LocationLatitude = "42.65", LocationLongitude = "18.09", Price = 120.0m });
            repository.InsertHotel(new Hotel { Id = 4, Name = "Hotel Rijeka", LocationLatitude = "45.33", LocationLongitude = "14.45", Price = 65.0m });
            repository.InsertHotel(new Hotel { Id = 5, Name = "Hotel Osijek", LocationLatitude = "45.55", LocationLongitude = "18.69", Price = 50.0m });
            repository.InsertHotel(new Hotel { Id = 6, Name = "Hotel Zadar", LocationLatitude = "44.12", LocationLongitude = "15.23", Price = 75.0m });
            repository.InsertHotel(new Hotel { Id = 7, Name = "Hotel Pula", LocationLatitude = "44.87", LocationLongitude = "13.85", Price = 80.0m });
            repository.InsertHotel(new Hotel { Id = 8, Name = "Hotel Šibenik", LocationLatitude = "43.73", LocationLongitude = "15.91", Price = 60.0m });
            repository.InsertHotel(new Hotel { Id = 9, Name = "Hotel Varaždin", LocationLatitude = "46.31", LocationLongitude = "16.34", Price = 45.0m });
            repository.InsertHotel(new Hotel { Id = 10, Name = "Hotel Koprivnica", LocationLatitude = "46.16", LocationLongitude = "16.83", Price = 40.0m });
        }
    }
}
