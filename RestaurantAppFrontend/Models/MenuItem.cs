﻿namespace RestaurantAppFrontend.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsAvailable { get; set; }
        public string? ImageUrl { get; set; }   
    }
}
