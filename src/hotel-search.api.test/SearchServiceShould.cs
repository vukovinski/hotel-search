﻿namespace hotel_search.api.test;

public class SearchServiceShould
{
    [Fact]
    public void ReturnCorrectOrder()
    {

        var repository = new HotelSqliteDatabase("");
        var searchService = new HotelSearchService(repository); 
        var location = new Location { Latitude = 50, Longitude = 15 };

        // Same price - same distance
        repository.CreateTables();
        repository.InsertHotel(new Hotel { Id = 1, LocationLatitude = "50", LocationLongitude = "15", Price = 100, Name = "A" });
        repository.InsertHotel(new Hotel { Id = 2, LocationLatitude = "50", LocationLongitude = "15", Price = 100, Name = "B" });

        var results1 = searchService.GetHotels(location);
        Assert.True(results1.First().Id == 1);
        Assert.True(results1.Last().Id == 2);
        repository.DeleteTables();


        // Same price - Different distance
        repository.CreateTables();
        repository.InsertHotel(new Hotel { Id = 3, LocationLatitude = "50", LocationLongitude = "15", Price = 100, Name = "C" });
        repository.InsertHotel(new Hotel { Id = 4, LocationLatitude = "51", LocationLongitude = "16", Price = 100, Name = "D" });

        var results2 = searchService.GetHotels(location);
        Assert.True(results2.First().Id == 3);
        Assert.True(results2.Last().Id == 4);
        repository.DeleteTables();

        // Different price - Same distance
        repository.CreateTables();
        repository.InsertHotel(new Hotel { Id = 5, LocationLatitude = "50", LocationLongitude = "15", Price = 100, Name = "E" });
        repository.InsertHotel(new Hotel { Id = 6, LocationLatitude = "50", LocationLongitude = "15", Price = 200, Name = "F" });

        var results3 = searchService.GetHotels(location);
        Assert.True(results3.First().Id == 5);
        Assert.True(results3.Last().Id == 6);
        repository.DeleteTables();


        // Different price - Different distance
        repository.CreateTables();
        repository.InsertHotel(new Hotel { Id = 7, LocationLatitude = "49", LocationLongitude = "15", Price = 100, Name = "G" });
        repository.InsertHotel(new Hotel { Id = 8, LocationLatitude = "48", LocationLongitude = "15", Price = 200, Name = "H" });

        var results4 = searchService.GetHotels(location);
        Assert.True(results4.First().Id == 7);
        Assert.True(results4.Last().Id == 8);
        repository.DeleteTables();

        // Combined
        repository.CreateTables();
        repository.InsertHotel(new Hotel { Id = 1, LocationLatitude = "50", LocationLongitude = "15", Price = 100, Name = "A" });
        repository.InsertHotel(new Hotel { Id = 2, LocationLatitude = "50", LocationLongitude = "15", Price = 100, Name = "B" });
        repository.InsertHotel(new Hotel { Id = 3, LocationLatitude = "50", LocationLongitude = "15", Price = 100, Name = "C" });
        repository.InsertHotel(new Hotel { Id = 4, LocationLatitude = "51", LocationLongitude = "16", Price = 100, Name = "D" });
        repository.InsertHotel(new Hotel { Id = 5, LocationLatitude = "50", LocationLongitude = "15", Price = 100, Name = "E" });
        repository.InsertHotel(new Hotel { Id = 6, LocationLatitude = "50", LocationLongitude = "15", Price = 200, Name = "F" });
        repository.InsertHotel(new Hotel { Id = 7, LocationLatitude = "49", LocationLongitude = "15", Price = 100, Name = "G" });
        repository.InsertHotel(new Hotel { Id = 8, LocationLatitude = "48", LocationLongitude = "15", Price = 200, Name = "H" });

        var results5 = searchService.GetHotels(location);
        Assert.True(results5[0].Id == 1);
        Assert.True(results5[1].Id == 2);
        Assert.True(results5[2].Id == 3);
        Assert.True(results5[3].Id == 5);
        Assert.True(results5[4].Id == 6);
        Assert.True(results5[5].Id == 7);
        Assert.True(results5[6].Id == 4);
        Assert.True(results5[7].Id == 8);
        repository.DeleteTables();
    }
}