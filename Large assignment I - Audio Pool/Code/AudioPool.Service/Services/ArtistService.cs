using AudioPool.Models.DTOs;
using AudioPool.Models;
using AudioPool.Repository.Interfaces;
using AudioPool.Service.Interfaces;
using AudioPool.Models.InputModels;

namespace AudioPool.Service.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public Envelope<ArtistDto> GetAllArtists(int pageNumber, int pageSize)
        {
            var artists = _artistRepository.GetAllArtists(pageNumber, pageSize);
            return artists;
        }

        public ArtistDetailsDto GetArtistById(int artistsId)
        {
            var artist = _artistRepository.GetArtistById(artistsId);
            return artist;
        }

        public List<AlbumDto> GetAlbumsByArtistsId(int Id)
        {
            var artist_albums = _artistRepository.GetAlbumsByArtistsId(Id);
            return artist_albums;
        }


        public int CreateNewArtist(ArtistInputModel artistInputModel)
        {
            return _artistRepository.CreateNewArtist(artistInputModel);
        }


        public void UpdateArtist(int id, ArtistInputModel artist)
        {
            _artistRepository.UpdateArtist(id, artist);
        }

        public void LinkArtistToGenre(int artistId, int genreId)
        {
            _artistRepository.LinkArtistToGenre(artistId, genreId);
        }


    }

}