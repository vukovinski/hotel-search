namespace hotel_search.api
{
    public interface IHotelSearchRepository : IDisposable
    {
        public int GetNextId();
        public List<Hotel> GetAll();
        public Hotel GetById(int id);

        public bool DeleteHotel(Hotel hotel);
        public bool InsertHotel(Hotel hotel);
        public bool UpdateHotel(Hotel hotel);
    }
}
