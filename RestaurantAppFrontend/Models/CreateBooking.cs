namespace RestaurantAppFrontend.Models
{
    public class CreateBooking
    {
        public int Id { get; set; }
        public int PartySize { get; set; }
        public DateTime RequestedDateTime { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
    }
}
