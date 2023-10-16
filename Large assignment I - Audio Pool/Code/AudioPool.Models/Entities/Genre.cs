using System;
using System.Collections.Generic;

namespace AudioPool.Models.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        // medaData
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; }
        public string? ModifiedBy { get; set; }

        // Navigation properties
        // public List<ArtistGenre> ArtistGenre { get; set; } = new List<ArtistGenre>();
        public ICollection<ArtistGenre> ArtistGenre { get; set; }
    }
}