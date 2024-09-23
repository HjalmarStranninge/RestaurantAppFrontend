namespace RestaurantAppFrontend.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsAvailable { get; set; }
        public string? ImageUrl { get; set; }
        public string Description { get; set; }
        public bool IsAppetizer { get; set; }
        public bool IsMainCourse { get; set; }
        public bool IsDesert { get; set; }
        public bool IsCocktail { get; set; }
    }
}
