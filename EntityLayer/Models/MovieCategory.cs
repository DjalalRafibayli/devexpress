using System;
using System.Collections.Generic;

namespace EntityLayer.Models
{
    public partial class MovieCategory
    {
        public MovieCategory()
        {
            Movies = new HashSet<Movie>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Preview { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
