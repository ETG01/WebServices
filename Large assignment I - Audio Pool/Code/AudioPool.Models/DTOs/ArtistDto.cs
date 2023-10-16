namespace AudioPool.Models.DTOs
{
    public class ArtistDto : HyperMediaModel
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
    }
    
}





