using AudioPool.Models.DTOs;
using AudioPool.Models;
using AudioPool.Repository.Interfaces;
using AudioPool.Service.Interfaces;
using AudioPool.Models.InputModels;

namespace AudioPool.Service.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public AlbumDetailsDto GetAlbumById(int id)
        {
            var album = _albumRepository.GetAlbumById(id);
            return album;
        }

        public List<SongDto> GetSongsOnAlbum(int albumId)
        {
            var songs = _albumRepository.GetSongsOnAlbum(albumId);
            return songs;
        }
        
        public int CreateAlbum(AlbumInputModel albumInputModel)
        {
            var albumId = _albumRepository.CreateAlbum(albumInputModel);
            return albumId;
        }
        
        public async Task<bool> DeleteAlbum(int id)
        {
            var result = await _albumRepository.DeleteAlbum(id);
            return result;
        }

    }
}