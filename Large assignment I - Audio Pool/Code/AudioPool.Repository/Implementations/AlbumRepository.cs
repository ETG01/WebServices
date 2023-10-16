using AutoMapper;
using AudioPool.Models.DTOs;
using AudioPool.Models.Entities;
using AudioPool.Models.InputModels;
using AudioPool.Repository.Data;
using AudioPool.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using AudioPool.Models;  // Add this to access the Envelope class
using System.Linq;

public class AlbumRepository : IAlbumRepository
    {
        private readonly AudioPoolDbContext _context;
        private readonly IMapper _mapper;

        public AlbumRepository(AudioPoolDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AlbumDetailsDto GetAlbumById(int id)
        {
            var album = _context.Albums
                .Include(a => a.AlbumArtists)
                .ThenInclude(aa => aa.Artist)
                .Include(a => a.Songs)
                .FirstOrDefault(a => a.Id == id);

            if (album == null)
            {
                return null;
            }

            var albumDetailsDto = _mapper.Map<AlbumDetailsDto>(album);

            // Populate _links property in each nested DTO (artists and songs)
            foreach(var artist in albumDetailsDto.Artists)
            {
                artist.Links.AddReference("self", $"/api/artists/{artist.Id}");
            }
        
            foreach(var song in albumDetailsDto.Songs)
            {
                song.Links.AddReference("self", $"/api/songs/{song.Id}");
            }

            // Add hypermedia links to the album DTO
            albumDetailsDto.Links.AddReference("self", $"/api/albums/{albumDetailsDto.Id}");
            albumDetailsDto.Links.AddReference("delete", $"/api/albums/{albumDetailsDto.Id}");
            albumDetailsDto.Links.AddReference("songs", $"/api/albums/{albumDetailsDto.Id}/songs");
            albumDetailsDto.Links.AddListReference("artists", album.AlbumArtists.Select(aa => $"/api/artists/{aa.ArtistsId}").ToArray());

            return albumDetailsDto;
        }
        
        public List<SongDto> GetSongsOnAlbum(int albumId)
        {
            var songs = _context.Albums
                .Include(a => a.Songs)
                .FirstOrDefault(a => a.Id == albumId)?
                .Songs;

            if (songs == null || !songs.Any())
            {
                return null;
            }

            var songDtos = songs.Select(song => 
                {
                    var songDto = _mapper.Map<SongDto>(song);
                    songDto.Links.AddReference("self", $"/api/songs/{songDto.Id}");
                    songDto.Links.AddReference("delete", $"/api/songs/{songDto.Id}");
                    songDto.Links.AddReference("edit", $"/api/songs/{songDto.Id}");
                    songDto.Links.AddReference("album", $"/api/albums/{albumId}");
        
                    return songDto;
                })
                .ToList();

            return songDtos;
        }
        
        public int CreateAlbum(AlbumInputModel albumInputModel)
        {
            // Map AlbumInputModel to Album entity (without AlbumArtist entities)
            var album = _mapper.Map<Album>(albumInputModel);

            // Create AlbumArtist entities for each artist ID in the input model
            foreach (var artistId in albumInputModel.ArtistIds)
            {
                var albumArtist = new AlbumArtist
                {
                    Album = album,
                    ArtistsId = artistId
                };
                album.AlbumArtists.Add(albumArtist);
            }

            _context.Albums.Add(album);
            _context.SaveChanges();

            return album.Id; // return the ID of the newly created album
        }

        public async Task<bool> DeleteAlbum(int id)
        {
            // Find the album by its ID
            var album = await _context.Albums
                .Include(a => a.AlbumArtists)
                .Include(a => a.Songs)
                .FirstOrDefaultAsync(a => a.Id == id);

            // If the album does not exist, return false
            if (album == null)
            {
                return false;
            }

            // Remove the album from the context
            _context.Albums.Remove(album);

            // Persist the changes to the database
            await _context.SaveChangesAsync();

            return true;
        }



    }

