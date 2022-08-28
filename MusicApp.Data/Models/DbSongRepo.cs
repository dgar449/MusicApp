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
            var song = context.Song
                .GroupBy(x=>x.ArtistID , (key, value) => new RankSongCountVm()
                {
                    ArtistID = key,
                    SongCount = value.Count()
                });
            var artist = context.Artist
                .Where(x => song.Any(y => y.ArtistID == x.ArtistID))
                .Select(x => new RankSongCountVm()
                {
                    ArtistID = x.ArtistID,
                    ArtistName = x.ArtistName,
                    SongCount = song.FirstOrDefault(y => y.ArtistID == x.ArtistID).SongCount
                }).OrderByDescending(x=>x.SongCount);
            Console.WriteLine(artist);
            return artist;
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