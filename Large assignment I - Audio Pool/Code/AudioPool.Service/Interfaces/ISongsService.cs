using AudioPool.Models.DTOs;
using System.Collections.Generic;
using AudioPool.Models.InputModels;

namespace AudioPool.Service.Interfaces
{
    public interface ISongsService
    { 
        SongDetailsDto GetSongById(int songId);
        
        Task<bool> DeleteSong(int id);
        
        bool UpdateSong(int id, SongInputModel songInputModel);
        
        int CreateSong(SongInputModel songInputModel);
    }
}