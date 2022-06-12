using MusicApp.Controllers;

namespace MusicApp.Models
{
    public interface ISongRepo
    {
        public IEnumerable<Song> GetAllSongs();
        public Song GetSongs(int Id);
        public Song Add(Song song);
        public Song Update(Song song);

        public Song Delete(int id);
       // bool Update(int id, string s, string r);
       // bool Delete(int id);
    }
}
