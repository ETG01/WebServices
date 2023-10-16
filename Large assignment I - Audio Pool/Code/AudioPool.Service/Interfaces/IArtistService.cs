using AudioPool.Models.DTOs;
using AudioPool.Models.InputModels;
using AudioPool.Models;

namespace AudioPool.Service.Interfaces
{
    public interface IArtistService
    {
        // ○ Get all artists
        Envelope<ArtistDto> GetAllArtists(int pageNumber, int pageSize);

        // ○ Get artist by id
        ArtistDetailsDto GetArtistById(int artistsId);

        // ○ Get artist albums by artist id

        List<AlbumDto> GetAlbumsByArtistsId(int Id);

        // ○ Create artist
        public int CreateNewArtist(ArtistInputModel artistInputModel);

        // // ○ Update artist by id
        void UpdateArtist(int id, ArtistInputModel artist);

        //
        // // ○ Link artist to genre
        void LinkArtistToGenre(int artistId, int genreId);


    }
}