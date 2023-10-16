using System.Dynamic;
using AudioPool.Models;
using AudioPool.Models.DTOs;
using AudioPool.Models.Entities;
using AudioPool.Models.InputModels;
using AudioPool.Repository.Data;
using AudioPool.Repository.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AudioPool.Repository.Implementations;

public class GenresRepository : IGenresRepository
{
    private readonly AudioPoolDbContext _context;
    private readonly IMapper _mapper;

    public GenresRepository(AudioPoolDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    // ------------------------- Get All Genres ----------------------------
    public IEnumerable<GenreDto> GetAllGenres()
    {
        var genres = _context.Genres
            .Include(g => g.ArtistGenre)
            .ThenInclude(ag => ag.Artist);

        var genresPage = genres.ToList();
        
        // Mapping to DTOs
        var genresDtos = _mapper.Map<IEnumerable<GenreDto>>(genresPage);

        foreach (var genresDto in genresDtos)
        {
            genresDto.Links = new ExpandoObject();
            
            genresDto.Links.AddReference("self", $"/api/genres/{genresDto.Id}");
            
            // Retrieve artist IDs
            var artistIds = _context.ArtistGenre
                .Where(ag => ag.GenresId == genresDto.Id)
                .Select(ag => ag.ArtistsId)
                .ToList();
            
            
            var artistLinks = artistIds.Select(id => $"/api/artists/{id}").ToArray();
            genresDto.Links.AddListReference("artists", artistLinks);
            
            // genresDto.Links.AddReference("artists", $"/api/artists/{artistIds[0]}");
        }

        return genresDtos;
    }
    // ------------------------- Get Genre By ID ----------------------------
    public IEnumerable<GenreDetailsDto> GetGenresById(int genresId)
    {
        var genre = _context.Genres
            .Include(g => g.ArtistGenre)
            .ThenInclude(ag => ag.Artist)
            .FirstOrDefault(g => g.Id == genresId);

        if (genre == null)
        {
            return null;
        }
        
        // Mapping to DTO
        var genreDto = _mapper.Map<GenreDetailsDto>(genre);
        
        genreDto.Links = new ExpandoObject();
            
        genreDto.Links.AddReference("self", $"/api/genres/{genreDto.Id}");
            
        // Retrieve artist IDs
        var artistIds = _context.ArtistGenre
            .Where(ag => ag.GenresId == genreDto.Id)
            .Select(ag => ag.ArtistsId)
            .ToList();
            
        genreDto.Links.AddReference("artists", $"/api/artists/{artistIds[0]}");
        
        return new List<GenreDetailsDto> { genreDto };
    }
    
    // ------------------------- Create Genre ----------------------------
    public int CreateGenre(GenreInputModel genreInp)
    {
        var genre = _mapper.Map<Genre>(genreInp);
        _context.Genres.Add(genre);
        _context.SaveChanges();

        return genre.Id;
    }
}