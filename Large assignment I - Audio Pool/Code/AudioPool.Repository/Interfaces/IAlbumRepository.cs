using AudioPool.Models.DTOs;
using AudioPool.Models.InputModels;


namespace AudioPool.Repository.Interfaces
{
    public interface IAlbumRepository
    {
        AlbumDetailsDto GetAlbumById(int id);

        List<SongDto> GetSongsOnAlbum(int albumId);
        
        // create a new album
        
        int CreateAlbum(AlbumInputModel albumInputModel);
        
        Task<bool> DeleteAlbum(int id);


    }
}