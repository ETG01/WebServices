using System;
using System.Collections.Generic;

namespace AudioPool.Models.Entities
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? CoverImageUrl { get; set; }
        public string? Description { get; set; }
        
        // medaData
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; }
        public string? ModifiedBy { get; set; }

        // Navigation properties
        public List<Song> Songs { get; set; } = new List<Song>();
        public List<AlbumArtist> AlbumArtists { get; set; } = new List<AlbumArtist>();
    }
    
}