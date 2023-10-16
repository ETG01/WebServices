using System.Dynamic;
using AudioPool.Models;
using AudioPool.Models.DTOs;
using AudioPool.Repository.Data;
using AudioPool.Repository.Interfaces;
using AudioPool.Models.Entities;
using AudioPool.Models.InputModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AudioPool.Repository.Implementations;

public class SongsRepository : ISongsRepository
{
    private readonly AudioPoolDbContext _context;
    private readonly IMapper _mapper;
    
    public SongsRepository(AudioPoolDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    // ------------------------- Get Song By ID ----------------------------
    public SongDetailsDto GetSongById(int songId)
    {
        var song = _context.Songs
            .Include(s => s.Album)
            .FirstOrDefault(g => g.Id == songId);;

        if (song == null)
        {
            return null;
        }
         // Mapping to DTOs
        var songDto = _mapper.Map<SongDetailsDto>(song);

        songDto.Links = new ExpandoObject();
        
        songDto.Links.AddReference("self", $"/api/songs/{songDto.Id}");
        songDto.Links.AddReference("delete", $"/api/songs/{songDto.Id}");
        songDto.Links.AddReference("edit", $"/api/songs/{songDto.Id}");
        songDto.Links.AddReference("album", $"/api/album/{songDto.Album.Id}");

        // Calculate the track number on the album
        var songsInAlbum = _context.Songs
            .Where(s => s.Album.Id == songDto.Album.Id)
            .ToList();

        songDto.TrackNumberOnAlbum = songsInAlbum.IndexOf(song) + 1;
        Console.WriteLine(songsInAlbum);
    
        return songDto;
    }
    
    // ------------------------- Delete Song by id ----------------------------
    
    public async Task<bool> DeleteSong(int id)
    {
        // Find the album by its ID
        var song = _context.Songs
            .Include(s => s.Album)
            .FirstOrDefault(g => g.Id == id);
        
        // If the album does not exist, return false
        if (song == null)
        {
            return false;
        }

        // Remove the song from the context
        _context.Songs.Remove(song);

        // Persist the changes to the database
        await _context.SaveChangesAsync();

        return true;
    }
    
    public bool UpdateSong(int id, SongInputModel songInputModel)
    {
        var song = _context.Songs
            .Include(s => s.Album)
            .FirstOrDefault(s => s.Id == id);

        if (song == null)
        {
            return false;
        }

        var album = _context.Albums.Find(songInputModel.AlbumId);
        if (album == null)
        {
            // The album ID provided does not exist
            return false;
        }

        // Update song properties with the new values
        song.Name = songInputModel.Name;
        song.Duration = songInputModel.Duration;
        song.AlbumId = songInputModel.AlbumId;   

        // Save changes
        _context.SaveChanges();

        return true;
    }
    
    public int CreateSong(SongInputModel songInputModel)
    {
        var song = _mapper.Map<Song>(songInputModel);
        _context.Songs.Add(song);
        _context.SaveChanges();

        return song.Id; // return the ID of the newly created song
    }
    
    


}