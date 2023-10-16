using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AudioPool.Models.Entities
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        
        // medaData
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; }
        public string? ModifiedBy { get; set; }

        // Foreign keys
        public int AlbumId { get; set; }

        // Navigation properties
        [ForeignKey("AlbumId")]
        public Album Album { get; set; }
    }
}