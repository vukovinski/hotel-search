using hotel_search.api;
namespace hotel_search.api.test;

public class SqliteRepositoryShould
{
    [Fact]
    public void ContainItemAfterInsert()
    {
        var repository = new HotelSqliteDatabase("Data Source=HotelSearch;Mode=Memory;Cache=Shared");
        repository.CreateTables();

        repository.InsertHotel(new Hotel { Id = 1, Name = "Sherwood", Price = 55m, LocationLatitude = "54", LocationLongitude = "31" });
        var hotels = repository.GetAll();


        Assert.True(hotels.Any());
        Assert.True(hotels[0].Id == 1);
    }
}
