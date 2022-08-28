﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MusicApp.Models;

#nullable disable

namespace MusicApp.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MusicApp.Controllers.Album", b =>
                {
                    b.Property<int>("AlbumID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AlbumID"), 1L, 1);

                    b.Property<string>("AlbumName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Length")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReleaseDate")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AlbumID");

                    b.ToTable("Album");
                });

            modelBuilder.Entity("MusicApp.Controllers.Artist", b =>
                {
                    b.Property<int>("ArtistID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArtistID"), 1L, 1);

                    b.Property<string>("ArtistName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ArtistID");

                    b.ToTable("Artist");
                });

            modelBuilder.Entity("MusicApp.Controllers.Genre", b =>
                {
                    b.Property<int>("GenreID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreID"), 1L, 1);

                    b.Property<string>("GenreType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GenreID");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("MusicApp.Controllers.Song", b =>
                {
                    b.Property<int>("SongID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SongID"), 1L, 1);

                    b.Property<int>("AlbumID")
                        .HasColumnType("int");

                    b.Property<int>("ArtistID")
                        .HasColumnType("int");

                    b.Property<int>("GenreID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SongTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrackLength")
                        .HasColumnType("int");

                    b.HasKey("SongID");

                    b.HasIndex("AlbumID");

                    b.HasIndex("ArtistID");

                    b.HasIndex("GenreID");

                    b.ToTable("Song");
                });

            modelBuilder.Entity("MusicApp.Controllers.Song", b =>
                {
                    b.HasOne("MusicApp.Controllers.Album", "Album")
                        .WithMany()
                        .HasForeignKey("AlbumID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MusicApp.Controllers.Artist", "Artist")
                        .WithMany()
                        .HasForeignKey("ArtistID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MusicApp.Controllers.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");

                    b.Navigation("Artist");

                    b.Navigation("Genre");
                });
#pragma warning restore 612, 618
        }
    }
}
