using Microsoft.EntityFrameworkCore;
using MusicApp.Controllers;
using MusicApp.Data.ViewModels;
using System.Linq;

namespace MusicApp.Models
{
    public class DbSongRepo : ISongRepo
    {
        public AppDbContext context;
        public DbSongRepo(AppDbContext context)
        {
            this.context = context;
        }

       /*public bool Delete(int id)
        {
            throw new NotImplementedException();
        }*/

        public Song Add(Song song)
        {
            context.Song.Add(song);
            context.SaveChanges();
            return song;
        }

        public Song Delete(int id)
        {
            Song song = context.Song.Find(id);
            if(song != null)
            {
                context.Song.Remove(song);
                context.SaveChanges();
            }
            return song;
        }

        public IEnumerable<Song> GetAllSongs()
        {
            return context.Song;
        }

        public Song GetSongs(int Id)
        {
            var song = context.Song.FirstOrDefault().SongID;
            return context.Song.Find(Id);
        }
        public IEnumerable<Artist> GetArtists()
        {
            return context.Artist;

        }


        public Song Update(Song songUpdates)
        {
            var song=context.Song.Attach(songUpdates);
            song.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return songUpdates;
        }
        public IEnumerable<SearchSongVm> Search(string query)
        {
            // LINQ
            //var song = from x in context.Song
            //           join y in context.Album on x.AlbumID equals y.AlbumID
            //           join z in context.Artist on x.ArtistID equals z.ArtistID
            //           where (x.SongTitle.Contains(sq) ||
            //            y.AlbumName.Contains(sq) ||
            //            z.ArtistName.Contains(sq))
            //           select x;


            // Lambda
            var song = context.Song
                .Include(x => x.Album)
                .Include(x => x.Artist)
                .Where(x => x.Artist.ArtistName.Contains(query)
                    || x.Album.AlbumName.Contains(query)
                    || x.SongTitle.Contains(query))
                .Select(x=>new SearchSongVm()
                {
                    AlbumName = x.Album.AlbumName,
                    ArtistName = x.Artist.ArtistName,
                    SongID = x.SongID,
                    TrackLength = x.TrackLength,
                    SongTitle = x.SongTitle
                });

            return song;
        }
        public IEnumerable<Artist> GetAlbum()
        {
            return context.Artist;

        }
        public IEnumerable<AlbumListVm> AllAlbums(int query)
        {
            //var albumCount = context.Album
            //    .GroupBy(x => x.ArtistID, (key, value) => new
            //    {
            //        ArtistID = key,
            //        Count = value.Count()
            //    });

            //var song = context.Artist
            //.Where(x => albumCount.Any(y => y.ArtistID == x.ArtistID))
            //.Select(x => new AlbumListVm()
            //{
            //    ArtistID = x.ArtistID,
            //    AlbumName = x.ArtistName,
            //    Count = albumCount.FirstOrDefault(y => y.ArtistID == x.ArtistID).Count
            //});
            var song = context.Album
             //   .Include(x => x.Artist)
                .Where(x => x.ArtistID==query)
                .Select(x => new AlbumListVm()
                {
                    AlbumName = x.AlbumName
                });
            return song;
        }

         public IEnumerable<RankSongCountVm> RankSongsTotal()
         {
            //Groups By Count of ArtistID in the Song Table
            var song = context.Song
                .GroupBy(x=>x.ArtistID , (key, value) => new RankSongCountVm()
                {
                    ArtistID = key,
                    SongCount = value.Count()
                });
            //Uses the results from the var song and adds artist name to it using FK ArtistID found in Song table and Artist table
            var artist = context.Artist
                .Where(x => song.Any(y => y.ArtistID == x.ArtistID))                
                .Select(x => new RankSongCountVm()
                {
                    ArtistID = x.ArtistID,
                    ArtistName = x.ArtistName,
                    SongCount = song.FirstOrDefault(y => y.ArtistID == x.ArtistID).SongCount
                }).ToList();
            //Ranks the results of var artist based on descending order and adds a rank to it (foreach ish)
            var rankings = artist.OrderByDescending(x => x.SongCount).GroupBy(x => x.SongCount)
                .SelectMany((g, i) => g.Select(e => new RankSongCountVm()
                {
                    ArtistName= e.ArtistName,
                    SongCount = e.SongCount,
                    Rank = i + 1
                }));
            return rankings;
         }
        public IEnumerable<MusicCareer> CareerAge()
        {
            //
            var song = context.Song
                .GroupBy(x => x.ArtistID, (key, value) => new MusicCareer()
                {
                    ArtistID = key,
                    Oldest = (DateTime)value.Min(y=> y.ReleaseDate)
                });
            //
            var artist = context.Artist
                .Where(x => song.Any(y => y.ArtistID == x.ArtistID))
                .Select(x => new MusicCareer()
                {
                    ArtistID = x.ArtistID,
                    ArtistName = x.ArtistName,
                    ReleaseDate = song.FirstOrDefault(y => y.ArtistID == x.ArtistID).Oldest
                }).ToList();

            var careerAge = artist.OrderBy(x => x.ReleaseDate).GroupBy(x => x.ReleaseDate)
             .SelectMany((g, i) => g.Select(e => new MusicCareer()
             {
                 ArtistName = e.ArtistName,
                 ReleaseDate = e.ReleaseDate,
                 Rank = i + 1
             }));
            return careerAge;
        }
        public IEnumerable<MusicCareer> CareerDuration()
        {
            //
            var song = context.Song
                .GroupBy(x => x.ArtistID, (key, value) => new MusicCareer()
                {
                    ArtistID = key,
                    Oldest = (DateTime)value.Min(y => y.ReleaseDate),
                    Newest = (DateTime)value.Max(y => y.ReleaseDate)
                });
            //
            var artist = context.Artist
                .Where(x => song.Any(y => y.ArtistID == x.ArtistID))
                .Select(x => new MusicCareer()
                {
                    ArtistID = x.ArtistID,
                    ArtistName = x.ArtistName,
                    CareerLength = (song.FirstOrDefault(y => y.ArtistID == x.ArtistID).Newest).Date - (song.FirstOrDefault(z => z.ArtistID == x.ArtistID).Oldest).Date
                }).ToList();

            var careerAge = artist.OrderByDescending(x => x.CareerLength).GroupBy(x => x.CareerLength)
             .SelectMany((g, i) => g.Select(e => new MusicCareer()
             {
                 ArtistName = e.ArtistName,
                 CareerDays = e.CareerLength.TotalDays,
                 Rank = i + 1
             }));
            return careerAge;
        }
        public IEnumerable<GenrePopularityVm> GenrePopularity()
        {
            //
            var song = context.Song
                .GroupBy(x => x.GenreID, (key, value) => new GenrePopularityVm()
                {
                    GenreID = key,
                    GenreCount = value.Count()
                });
            //
            var genre = context.Genre
                .Where(x => song.Any(y => y.GenreID == x.GenreID))
                .Select(x => new GenrePopularityVm()
                {
                    GenreID = x.GenreID,
                    GenreType = x.GenreType,
                    GenreCount = song.FirstOrDefault(y => y.GenreID == x.GenreID).GenreCount
                }).ToList();
            //
            var rankings = genre.OrderByDescending(x => x.GenreCount).GroupBy(x => x.GenreCount)
                .SelectMany((g, i) => g.Select(e => new GenrePopularityVm()
                {
                    GenreType = e.GenreType,
                    GenreCount = e.GenreCount,
                    Rank = i + 1
                }));
            return rankings;
        }
    }
}


/*
 * albumCount
 * ArtistID     Count
 * 1            2
 * 2            2
 * 3            2
 * 
 * 
 * Artist table
 * ArtistID     ArtistName      WHERE(x => albumCount.Any(y => y.ArtistID == x.ArtistID))
 * 1            asldfhj         true
 * 2            sdfjh           false true
 * 3            sdfkj           false false true
 * 
 * 
 */