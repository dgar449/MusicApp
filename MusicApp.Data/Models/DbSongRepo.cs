using Microsoft.EntityFrameworkCore;
using MusicApp.Controllers;
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

        public Song GetSongs(int Id, int songId)
        {
            var song = context.Actors
                .FirstOrDefault(x => x.Id == Id)
                .Songs.FirstOrDefault(i => i.SongID == songId);
            //context.Song.FirstOrDefault().Artist;
            return context.Song.Find(Id);

        }

        public Song Update(Song songUpdates)
        {
            var song=context.Song.Attach(songUpdates);
            song.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return songUpdates;
        }
    }
}
