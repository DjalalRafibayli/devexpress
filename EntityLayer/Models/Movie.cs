using System;
using System.Collections.Generic;

namespace EntityLayer.Models
{
    public partial class Movie
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImagePath { get; set; }
        public string? TrailerUrl { get; set; }
        public int? Preview { get; set; }
        public int? Like { get; set; }
        public double? Rating { get; set; }
        public DateTime? RelaseDate { get; set; }
        public int? Category { get; set; }
        public bool? IsActive { get; set; }

        public virtual MovieCategory? CategoryNavigation { get; set; }
    }
}
