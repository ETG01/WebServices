using System.Text.Json.Serialization;

namespace AudioPool.Models.DTOs
{
    public class GenreDetailsDto : HyperMediaModel
    {
        // GenreDetailsDto
        // ○ Id (int)
        public int Id { get; set; }
        // ○ Name (string)
        public string Name { get; set; }
        // ○ NumberOfArtists (int) // fix
        public int NumberOfArtists { get; set; }
    }
}
