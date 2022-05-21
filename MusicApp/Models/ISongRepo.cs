using MusicApp.Controllers;

namespace MusicApp.Models
{
    public interface ISongRepo
    {
        IEnumerable<Song> GetAllSongs();
        Song GetSongs(int Id);
        Song Add(Song song);
        bool Update(int id, string s, string r);
        bool Delete(int id);
    }
}
