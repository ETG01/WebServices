using Microsoft.EntityFrameworkCore;
using AudioPool.Models.Entities;

namespace AudioPool.Repository.Data
{
    public class AudioPoolDbContext : DbContext
    {
        public AudioPoolDbContext(DbContextOptions<AudioPoolDbContext> options) : base(options)
        {
            
        }
    
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<AlbumArtist> AlbumArtist { get; set; }
        public DbSet<ArtistGenre> ArtistGenre { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define many-to-many relationship between Album and Artist
            modelBuilder.Entity<AlbumArtist>()
                .HasKey(aa => new { aa.AlbumsId, aa.ArtistsId });
            modelBuilder.Entity<AlbumArtist>()
                .HasOne(aa => aa.Album)
                .WithMany(a => a.AlbumArtists)
                .HasForeignKey(aa => aa.AlbumsId);
            modelBuilder.Entity<AlbumArtist>()
                .HasOne(aa => aa.Artist)
                .WithMany(a => a.AlbumArtist)
                .HasForeignKey(aa => aa.ArtistsId);

            // Define many-to-many relationship between Artist and Genre
            modelBuilder.Entity<ArtistGenre>()
                .HasKey(ag => new { ag.ArtistsId, ag.GenresId });
            modelBuilder.Entity<ArtistGenre>()
                .HasOne(ag => ag.Artist)
                .WithMany(a => a.ArtistGenre)
                .HasForeignKey(ag => ag.ArtistsId);
            modelBuilder.Entity<ArtistGenre>()
                .HasOne(ag => ag.Genre)
                .WithMany(g => g.ArtistGenre)
                .HasForeignKey(ag => ag.GenresId);

            // Define one-to-many relationship between Album and Song
            modelBuilder.Entity<Song>()
                .HasOne(s => s.Album)
                .WithMany(a => a.Songs)
                .HasForeignKey(s => s.AlbumId);
        }
    }
}
