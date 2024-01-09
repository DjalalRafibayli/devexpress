using System;
using System.Collections.Generic;

namespace EntityLayer.Models
{
    public partial class MovieBanner
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public DateTime RelaseDate { get; set; }
        public string TrailerUrl { get; set; } = null!;
        public string ImagePath { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
