
namespace AudioPool.Models.DTOs
{
    public class AlbumDto : HyperMediaModel
    {
        //Id (int)
        public int Id { get; set; }
        //Name (string)
        public string Name { get; set; }
        //ReleaseDate (datetime)
        public DateTime ReleaseDate { get; set; }
        //CoverImageUrl (string)
        public string? CoverImageUrl { get; set; }
        //Description (string)
        public string? Description { get; set; }
    }
}





