using System.Dynamic;
using AudioPool.Models;  // Add this to access the Envelope class
using AudioPool.Models.DTOs;
using AudioPool.Models.Entities;
using AudioPool.Models.InputModels;
using AudioPool.Repository.Data;
using Microsoft.EntityFrameworkCore;
using AudioPool.Repository.Interfaces;
using AutoMapper;

public class ArtistRepository : IArtistRepository
{
    private readonly AudioPoolDbContext _context;
    private readonly IMapper _mapper;

    public ArtistRepository(AudioPoolDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    // Get artists
    public Envelope<ArtistDto> GetAllArtists(int pageNumber, int pageSize)
    {
        var artists = _context.Artists // Assuming you have a DbContext
            .OrderBy(a => a.DateOfStart)
            .Include(a => a.AlbumArtist)
            .ThenInclude(aa => aa.Album) // Including Album of AlbumArtist
            .Include(a => a.ArtistGenre)
            .ThenInclude(ag => ag.Genre) // Including Genre of ArtistGenre
            .ToList();

        var artistDTOs = artists.Select(a => new ArtistDto 
        {
            Id = a.Id,
            Name = a.Name,
            Bio = a.Bio,
            CoverImageUrl = a.CoverImageUrl,
            DateOfStart = a.DateOfStart,        
        }).ToList();

        foreach (var artist in artistDTOs)
        {
            artist.Links.AddReference("self", $"/api/artists/{artist.Id}");
            artist.Links.AddReference("edit", $"/api/artists/{artist.Id}");
            artist.Links.AddReference("delete", $"/api/artists/{artist.Id}");
            artist.Links.AddReference("albums", $"/api/artists/{artist.Id}/albums");
    
            var genreLinks = artists.Where(a => a.Id == artist.Id).SelectMany(a => a.ArtistGenre).Select(ag => $"/api/genres/{ag.Genre.Id}").ToArray();
            artist.Links.AddListReference("genres", genreLinks);
        }


        var envelope = new Envelope<ArtistDto>(pageNumber, pageSize, artistDTOs);
        return envelope;
    }
    
    // Get artist by id
    public ArtistDetailsDto GetArtistById(int artistsId)
    {
        var artist = _context.Artists
            .Include(a => a.AlbumArtist)
            .ThenInclude(aa => aa.Album)
            .Include(a => a.ArtistGenre)
            .ThenInclude(ag => ag.Genre)
            .FirstOrDefault(c => c.Id == artistsId);

                
        if (artist == null)
        {
            // Handle not found case, perhaps throw a not found exception or return null
            return null;
        }
        // Create artistDto using AutoMapper (I'm assuming you have a _mapper object)
        var artistDto = _mapper.Map<ArtistDetailsDto>(artist);

        // Add hypermedia links
        artistDto.Links.AddReference("self", $"/api/artists/{artistDto.Id}");
        artistDto.Links.AddReference("edit", $"/api/artists/{artistDto.Id}");
        artistDto.Links.AddReference("delete", $"/api/artists/{artistDto.Id}");
        artistDto.Links.AddReference("albums", $"/api/artists/{artistDto.Id}/albums");
        artistDto.Links.AddListReference("genres", artist.ArtistGenre.Select(ag => $"/api/genres/{ag.Genre.Id}").ToArray());

        return artistDto;
    }
    
    // Get artist albums

    public List<AlbumDto> GetAlbumsByArtistsId(int id)
    {
        var albums = _context.Artists
            .Include(a => a.AlbumArtist)
            .ThenInclude(aa => aa.Album)
            .FirstOrDefault(a => a.Id == id)?
            .AlbumArtist.Select(aa => aa.Album)
            .ToList();

        if (albums == null || !albums.Any())
        {
            // Handle not found case, perhaps throw a not found exception or return null
            return null;
        }

        var albumDtos = albums.Select(album => 
            {
                var albumDto = _mapper.Map<AlbumDto>(album);
                albumDto.Links.AddReference("self", $"/api/albums/{albumDto.Id}");
                albumDto.Links.AddReference("delete", $"/api/albums/{albumDto.Id}");
                albumDto.Links.AddReference("songs", $"/api/albums/{albumDto.Id}/songs");
                albumDto.Links.AddListReference("artists", new[] { $"/api/artists/{id}" });
        
                return albumDto;
            })
            .ToList();

        return albumDtos;
    }
    
    public int CreateNewArtist(ArtistInputModel artistInputModel)
    {
        var artist = _mapper.Map<Artist>(artistInputModel);
        _context.Artists.Add(artist);
        _context.SaveChanges();

        return artist.Id; // return the ID of the newly created artist
    }
    
    
    public void UpdateArtist(int id, ArtistInputModel artistInput)
    {
        var artist = _context.Artists.Find(id);
        if(artist != null)
        {
            artist.Name = artistInput.Name;
            artist.Bio = artistInput.Bio;
            artist.CoverImageUrl = artistInput.CoverImageUrl;
            artist.DateOfStart = artistInput.DateOfStart;
            // _context.Artists.Update(artist);
            _context.SaveChanges();
        }
        else
        {
            throw new InvalidOperationException("Artist not found");
        }
    }
    
    public void LinkArtistToGenre(int artistId, int genreId)
    {
        // Fetch the artist and genre to ensure they exist
        var artist = _context.Artists.Find(artistId);
        var genre = _context.Genres.Find(genreId);
        
        if (artist == null && genre == null)
        {
            throw new InvalidOperationException("Invalid artistId and genreId");
        }
        else if (artist == null)
        {
            throw new InvalidOperationException("Invalid artistId");
        }
        else if (genre == null)
        {
            throw new InvalidOperationException("Invalid genreId");
        }
        var existingLink = _context.ArtistGenre
            .FirstOrDefault(ag => ag.ArtistsId == artistId && ag.GenresId == genreId);

        if(existingLink != null)
        {
            throw new InvalidOperationException("Artist and genre already linked");
        }

        // Assuming ArtistGenres is the name of your linking entity
        var artistGenre = new ArtistGenre 
        { 
            ArtistsId = artistId, 
            GenresId = genreId 
        };
        
        _context.ArtistGenre.Add(artistGenre);
        _context.SaveChanges();
    }


}

