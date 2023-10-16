using AudioPool.Models.DTOs;
using AudioPool.Models.InputModels;
using AudioPool.Repository;
using AudioPool.Repository.Implementations;
using AudioPool.Repository.Interfaces;
using AudioPool.Service.Interfaces;

namespace AudioPool.Service; 
    public class GenresService : IGenresService
    {
        private readonly IGenresRepository _genrasRepository;

        public GenresService(IGenresRepository genrasRepository)
        {
            _genrasRepository = genrasRepository;
        }

        public IEnumerable<GenreDto> GetAllGenres()
        {
            var genres = _genrasRepository.GetAllGenres();
            return genres;
        }
        public IEnumerable<GenreDetailsDto> GetGenresById(int genresId)
        {
            var genres = _genrasRepository.GetGenresById(genresId);
            return genres;
        }

        public int CreateGenre(GenreInputModel genreInp)
        {
            return _genrasRepository.CreateGenre(genreInp);
        }
    }
