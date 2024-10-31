using Microsoft.Data.Sqlite;

namespace hotel_search.api;

public sealed class HotelSqliteDatabase : IHotelSearchRepository, IDisposable
{
    private readonly SqliteConnection _connection;

    public HotelSqliteDatabase(string connectionString)
    {
        var connection = new SqliteConnection(connectionString);
        _connection = connection;
        _connection.Open();
    }

    private static Hotel ReadHotel(SqliteDataReader reader)
    {
        return new Hotel
        {
            Id = reader.GetInt32(0),
            Name = reader.GetString(1),
            Price = decimal.Parse(reader.GetString(2)),
            LocationLatitude = reader.GetString(3),
            LocationLongitude = reader.GetString(4)
        };
    }

    public List<Hotel> GetAll()
    {
        var hotels = new List<Hotel>();
        using var command = _connection.CreateCommand();
        command.CommandText =
            @"
                SELECT id, name, price, locationLat, locationLon FROM Hotels
            ";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            hotels.Add(ReadHotel(reader));
        }
        return hotels;
    }

    public Hotel GetById(int id)
    {
        Hotel? hotel = null;
        using var command = _connection.CreateCommand();
        command.CommandText =
            @$"
                SELECT id, name, price, locationLat, locationLon FROM Hotels WHERE id = {id}
            ";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            hotel = ReadHotel(reader);
        }
        if (hotel == null)
            throw new KeyNotFoundException("Hotel not found.");
        return hotel;
    }

    public bool DeleteHotel(Hotel hotel)
    {
        using var command = _connection.CreateCommand();
        command.CommandText =
            @$"
                DELETE FROM Hotels WHERE id = {hotel.Id}
            ";
        return command.ExecuteNonQuery() == 1;
    }

    public bool InsertHotel(Hotel hotel)
    {
        using var command = _connection.CreateCommand();
        command.CommandText =
            @$"
                INSERT INTO Hotels (id, name, price, locationLat, locationLon)
                VALUES ({hotel.Id}, '{hotel.Name}', '{hotel.Price.ToString()}', '{hotel.LocationLatitude}', '{hotel.LocationLongitude.ToString()}')
            ";
        return command.ExecuteNonQuery() == 1;
    }

    public bool UpdateHotel(Hotel hotel)
    {
        using var command = _connection.CreateCommand();
        command.CommandText =
            @$"
                UPDATE Hotels
                SET name = '{hotel.Name}',
                    price = '{hotel.Price.ToString()}',
                    locationLat = '{hotel.LocationLatitude}',
                    locationLon = '{hotel.LocationLongitude}'
                WHERE id = {hotel.Id}
            ";
        return command.ExecuteNonQuery() == 1;
    }

    public int GetNextId()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = @"
            SELECT MAX(id) AS max_id FROM Hotels;
        ";
        return ((int)(long)command.ExecuteScalar()!) + 1;
    }

    public void CreateTables()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Hotels (
            id INTEGER PRIMARY KEY,
            name TEXT NOT NULL,
            price TEXT NOT NULL,
            locationLat TEXT NOT NULL,
            locationLon TEXT NOT NULL
            );
        ";
        command.ExecuteNonQuery();
    }

    public void DeleteTables()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = @"
            DROP TABLE Hotels;
        ";
        command.ExecuteNonQuery();
    }

    public void Dispose()
    {
        _connection.Close();
        _connection.Dispose();
    }
}
