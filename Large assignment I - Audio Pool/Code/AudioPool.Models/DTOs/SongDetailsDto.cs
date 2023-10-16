namespace AudioPool.Models.DTOs
{
    public class SongDetailsDto: HyperMediaModel
    {
        // ○ Id (int)
        public int Id { get; set; }
        // ○ Name (string)
        public string Name { get; set; }
        // ○ Duration (timespan)
        public TimeSpan Duration { get; set; }
        // ○ Album (AlbumDto)
        public AlbumDto Album { get; set; }
        // ○ TrackNumberOnAlbum (int) // check this
        public int TrackNumberOnAlbum { get; set; }

    }
}