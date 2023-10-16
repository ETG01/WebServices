﻿// <auto-generated />
using System;
using AudioPool.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AudioPool.Presentation.Migrations
{
    [DbContext(typeof(AudioPoolDbContext))]
    partial class AudioPoolDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("AudioPool.Models.Entities.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CoverImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("AudioPool.Models.Entities.AlbumArtist", b =>
                {
                    b.Property<int>("AlbumsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ArtistsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AlbumsId", "ArtistsId");

                    b.HasIndex("ArtistsId");

                    b.ToTable("AlbumArtist");
                });

            modelBuilder.Entity("AudioPool.Models.Entities.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Bio")
                        .HasColumnType("TEXT");

                    b.Property<string>("CoverImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateOfStart")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("AudioPool.Models.Entities.ArtistGenre", b =>
                {
                    b.Property<int>("ArtistsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GenresId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ArtistsId", "GenresId");

                    b.HasIndex("GenresId");

                    b.ToTable("ArtistGenre");
                });

            modelBuilder.Entity("AudioPool.Models.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("AudioPool.Models.Entities.Song", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AlbumId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("AudioPool.Models.Entities.AlbumArtist", b =>
                {
                    b.HasOne("AudioPool.Models.Entities.Album", "Album")
                        .WithMany("AlbumArtists")
                        .HasForeignKey("AlbumsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AudioPool.Models.Entities.Artist", "Artist")
                        .WithMany("AlbumArtist")
                        .HasForeignKey("ArtistsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("AudioPool.Models.Entities.ArtistGenre", b =>
                {
                    b.HasOne("AudioPool.Models.Entities.Artist", "Artist")
                        .WithMany("ArtistGenre")
                        .HasForeignKey("ArtistsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AudioPool.Models.Entities.Genre", "Genre")
                        .WithMany("ArtistGenre")
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("AudioPool.Models.Entities.Song", b =>
                {
                    b.HasOne("AudioPool.Models.Entities.Album", "Album")
                        .WithMany("Songs")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");
                });

            modelBuilder.Entity("AudioPool.Models.Entities.Album", b =>
                {
                    b.Navigation("AlbumArtists");

                    b.Navigation("Songs");
                });

            modelBuilder.Entity("AudioPool.Models.Entities.Artist", b =>
                {
                    b.Navigation("AlbumArtist");

                    b.Navigation("ArtistGenre");
                });

            modelBuilder.Entity("AudioPool.Models.Entities.Genre", b =>
                {
                    b.Navigation("ArtistGenre");
                });
#pragma warning restore 612, 618
        }
    }
}
