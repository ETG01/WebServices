namespace AudioPool.Models.DTOs
{

    public class AlbumDetailsDto : HyperMediaModel
    {
        // ○ Id (int)
        public int Id { get; set; }
        // ○ Name (string)
        public string Name { get; set; }
        // ○ ReleaseDate (datetime)
        public DateTime ReleaseDate { get; set; }
        // ○ CoverImageUrl (string)
        public string? CoverImageUrl { get; set; }
        // ○ Description (string)
        public string? Description { get; set; }
        // ○ Artists (IEnumerable<ArtistDto>)
        public IEnumerable<ArtistDto> Artists { get; set; }
        // ○ Songs (IEnumerable<SongDto>)
        public IEnumerable<SongDto> Songs { get; set; }
        
    }
}
