namespace hotel_search.api.test;

public class SqliteRepositoryShould
{
    [Fact]
    public void ContainItemAfterInsert()
    {
        using var repository = new HotelSqliteDatabase("Data Source=HotelSearch;Mode=Memory;");
        repository.CreateTables();

        repository.InsertHotel(new Hotel { Id = 1, Name = "Sherwood", Price = 55m, LocationLatitude = "54", LocationLongitude = "31" });
        var hotels = repository.GetAll();

        Assert.True(hotels.Count != 0);
        Assert.True(hotels[0].Id == 1);
        repository.DeleteTables();
    }

    [Fact]
    public void NotContainItemAfterDelete()
    {
        using var repository = new HotelSqliteDatabase("Data Source=HotelSearch;Mode=Memory;");
        repository.CreateTables();

        repository.InsertHotel(new Hotel { Id = 1, Name = "Sherwood", Price = 55m, LocationLatitude = "54", LocationLongitude = "31" });
        var hotels = repository.GetAll();

        Assert.True(hotels.Any());
        Assert.True(hotels[0].Id == 1);

        repository.DeleteHotel(new Hotel { Id = 1 });
        hotels = repository.GetAll();
        
        Assert.True(hotels.Count == 0);
        repository.DeleteTables();
    }
}
