namespace hotel_search.api
{
    public interface IHotelSearchRepository : IDisposable
    {
        public int GetNextId();
        public List<Hotel> GetAll();
        public Hotel GetById(int id);

        public void DeleteHotel(Hotel hotel);
        public void InsertHotel(Hotel hotel);
        public void UpdateHotel(Hotel hotel);
    }
}
