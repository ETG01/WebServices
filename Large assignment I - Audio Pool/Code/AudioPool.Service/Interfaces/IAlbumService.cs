using System.Collections.Generic;
using AudioPool.Models.DTOs;
using AudioPool.Models.InputModels;
using AudioPool.Models;


namespace AudioPool.Service.Interfaces
{
    public interface IAlbumService
    {
        AlbumDetailsDto GetAlbumById(int id);

        List<SongDto> GetSongsOnAlbum(int albumId);
        
        int CreateAlbum(AlbumInputModel albumInputModel);
        
        Task<bool> DeleteAlbum(int id);


    }
}