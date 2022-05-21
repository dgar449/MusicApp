using MusicApp.Controllers;

namespace MusicApp.Models
{
    public class DbSongRepo 
    {
        private readonly AppDbContext context;
        public DbSongRepo(AppDbContext context)
        {
            this.context = context;
        }

       /* public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        Song ISongRepo.Add(Song song)
        {
            context.Songs.Add(song);
            context.SaveChanges();
            return song;
        }

        Song ISongRepo.Delete(int id)
        {
            Song song = context.Songs.Find(id);
            if(song != null)
            {
                context.Songs.Remove(song);
                context.SaveChanges();
            }
            return song;
        }

        IEnumerable<Song> ISongRepo.GetAllSongs()
        {
            return context.Songs;
        }

        Song ISongRepo.GetSongs(int Id)
        {
            return context.Songs.Find(Id);
        }

        Song ISongRepo.Update(Song songUpdates)
        {
            var song=context.Songs.Attach(songUpdates);
            song.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return songUpdates;
        }*/
    }
}
