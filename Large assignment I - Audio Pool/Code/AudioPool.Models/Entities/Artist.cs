using System;
using System.Collections.Generic;

namespace AudioPool.Models.Entities
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Bio { get; set; }
        public string? CoverImageUrl { get; set; }
        public DateTime DateOfStart { get; set; }
        
        // medaData
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; }
        public string? ModifiedBy { get; set; }

        // Navigation properties
        public List<AlbumArtist> AlbumArtist { get; set; } = new List<AlbumArtist>();
        // public List<ArtistGenre> ArtistGenre { get; set; } = new List<ArtistGenre>();
        public ICollection<ArtistGenre> ArtistGenre { get; set; }
    }
    
}