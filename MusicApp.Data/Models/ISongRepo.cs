using MusicApp.Controllers;
using MusicApp.Data.ViewModels;

namespace MusicApp.Models
{
    public interface ISongRepo
    {
        public IEnumerable<Song> GetAllSongs();
        public Song GetSongs(int Id);
        public Song Add(Song song);
        public Song Update(Song song);

        public Song Delete(int id);
        public IEnumerable<SearchSongVm> Search(string sq);

        public IEnumerable<RankSongCountVm> RankSongsTotal();

        public IEnumerable<AlbumListVm> AllAlbums(int sq);

        public IEnumerable<Artist> GetArtists();
        public IEnumerable<Artist> GetAlbum();
        // public AppDbContext ArtistList();
        // bool Update(int id, string s, string r);
        // bool Delete(int id);
    }
}
