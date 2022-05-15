using MusicApp.Controllers;

namespace MusicApp.Models
{
    public interface ISongRepo
    {
        IEnumerable<Song> GetAllSongs();
        Song GetSongs(int Id);
        Song Add(Song song);
        Song Update(Song songUpdates);
        bool Delete(int id);
    }
}
