using AudioPool.Models.DTOs;
using System.Collections.Generic;
using AudioPool.Models.InputModels;

namespace AudioPool.Service.Interfaces
{
    public interface IGenresService
    {
        IEnumerable<GenreDto> GetAllGenres();
        IEnumerable<GenreDetailsDto> GetGenresById(int genreId);

        public int CreateGenre(GenreInputModel genreInp);
    }
}