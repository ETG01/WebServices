using AudioPool.Models.DTOs;
using System.Collections.Generic;
using AudioPool.Models.InputModels;

namespace AudioPool.Repository.Interfaces
{
    public interface ISongsRepository
    { 
        SongDetailsDto GetSongById(int songId);
        
        Task<bool> DeleteSong(int id);
        
        bool UpdateSong(int id, SongInputModel songInputModel);
        
        
        int CreateSong(SongInputModel songInputModel);

    }   
}