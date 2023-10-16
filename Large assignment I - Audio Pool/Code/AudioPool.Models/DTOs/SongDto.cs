namespace AudioPool.Models.DTOs
{
    public class SongDto: HyperMediaModel
    {
        // ○ Id (int)
        public int Id { get; set; }
        // ○ Name (string)
        public string Name { get; set; }
        // ○ Duration (timespan)
        public TimeSpan Duration { get; set; }
    }

}
