namespace hotel_search.api;

public class Hotel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string LocationLatitude { get; set; }
    public string LocationLongitude { get; set; }
}

public class HotelWithDistance : Hotel
{
    public double Distance { get; set; }
}