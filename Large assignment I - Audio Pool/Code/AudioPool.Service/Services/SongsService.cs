using AudioPool.Models.DTOs;
using AudioPool.Repository.Interfaces;
using AudioPool.Service.Interfaces;
using AudioPool.Models.InputModels;

namespace AudioPool.Service.Services
{
    public class SongsService : ISongsService
    {
        private readonly ISongsRepository _songsRepository;

        public SongsService(ISongsRepository songsRepository)
        {
            _songsRepository = songsRepository;
        }

        public SongDetailsDto GetSongById(int songId)
        {
            var song = _songsRepository.GetSongById(songId);
            return song;
        }
        

        public async Task<bool> DeleteSong(int id)
        {
            var result = await _songsRepository.DeleteSong(id);
            return result;
        }
        
        public bool UpdateSong(int id, SongInputModel songInputModel)
        {
            var result = _songsRepository.UpdateSong(id, songInputModel);
            return result;
        }
        
        
        public int CreateSong(SongInputModel songInputModel)
        {
            return _songsRepository.CreateSong(songInputModel);
        }
        
    }
}

