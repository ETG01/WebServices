using AutoMapper;
using AudioPool.Repository.Data;
using AudioPool.Models.DTOs;
using AudioPool.Models.InputModels;
using AudioPool.Models.Entities;

namespace AudioPool.Presentation.Profiles
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ... (add other mappings as needed)
            
            // ----- Genre -----
            CreateMap<Genre, GenreDto>();
            CreateMap<Genre, GenreDetailsDto>();
            CreateMap<GenreInputModel, Genre>();
            
            // ---- Artist -----
            CreateMap<ArtistGenre, ArtistDto>();
            CreateMap<ArtistInputModel, Artist>();
            CreateMap<Artist, ArtistDto>();
            CreateMap<Artist, ArtistDetailsDto>()
                .ForMember(dest => dest.Albums, opt => opt.MapFrom(src => src.AlbumArtist.Select(aa => aa.Album)))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.ArtistGenre.Select(ag => ag.Genre)));
            CreateMap<ArtistInputModel, Artist>()
                .ForMember(dest => dest.DateOfStart, opts => opts.MapFrom(src => src.DateOfStart));
            
            // ---- Album -----
            CreateMap<Album, AlbumDto>();
            CreateMap<AlbumInputModel, Album>();
            CreateMap<Album, AlbumDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
                .ForMember(dest => dest.CoverImageUrl, opt => opt.MapFrom(src => src.CoverImageUrl))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.AlbumArtists.Select(aa => aa.Artist))) // Mapping the artist details from AlbumArtists
                .ForMember(dest => dest.Songs, opt => opt.MapFrom(src => src.Songs)); // Mapping the songs directly
            
            // ---- Song ----
            CreateMap<Song, SongDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<Song, SongDetailsDto>();
            
            CreateMap<SongInputModel, Song>();
        }
    }
}