﻿using Microsoft.AspNetCore.Identity;

namespace ExpirationDate.Models.Entitys
{
    public class Item
    {
        public int ItemId { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Rating { get; set; }
    }
}