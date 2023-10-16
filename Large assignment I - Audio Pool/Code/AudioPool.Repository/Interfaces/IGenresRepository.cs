using AudioPool.Models;
using AudioPool.Models.DTOs;
using AudioPool.Models.InputModels;

namespace AudioPool.Repository.Interfaces
{
    public interface IGenresRepository
    {
        IEnumerable<GenreDto> GetAllGenres();
        IEnumerable<GenreDetailsDto> GetGenresById(int genresId);

        int CreateGenre(GenreInputModel genreInp);

    }
}