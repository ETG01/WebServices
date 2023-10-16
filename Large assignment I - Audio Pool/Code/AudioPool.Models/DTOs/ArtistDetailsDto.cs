using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace AudioPool.Models.DTOs
{
    public class ArtistDetailsDto : HyperMediaModel
    {
        // ○ Id (int)
        public int Id { get; set; }
        // ○ Name (string)
        public string Name { get; set; }
        // ○ Bio (string)
        public string? Bio { get; set; }
        // ○ CoverImageUrl (string)
        public string? CoverImageUrl { get; set; }
        // ○ DateOfStart (datetime)
        public DateTime DateOfStart { get; set; }
        // ○ Albums (IEnumerable<AlbumDto>) later
        
        public IEnumerable<AlbumDto> Albums { get; set; }
        public IEnumerable<GenreDto> Genres { get; set; }
    }
}