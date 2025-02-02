namespace RestaurantAppFrontend.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int PartySize { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int CustomerId { get; set; }
        public int TableId { get; set; }
    }
}
