using MusicApp.Controllers;
using MusicApp.Data.ViewModels;

namespace MusicApp.Models
{
    public interface ISongRepo
    {
         IEnumerable<Song> GetAllSongs();
         Song GetSongs(int Id);
         Song Add(Song song);
         Song Update(Song song);

         Song? Delete(int id);
         Task<IEnumerable<SearchSongVm>> Search(string sq);

         Task<IEnumerable<RankSongCountVm>> RankSongsTotal();
         Task<IEnumerable<MusicCareer>> CareerAge();
         Task<IEnumerable<MusicCareer>> CareerDuration();
         Task<IEnumerable<MusicCareer>> CareerAverage();
         Task<IEnumerable<GenrePopularityVm>> GenrePopularity();

         Task<IEnumerable<GenreArtistPopularityVm>> GenreArtistPopularity();
         Task<IEnumerable<AlbumListVm>> AllAlbums(int sq);

         IEnumerable<Artist> GetArtists();
         IEnumerable<Artist> GetAlbum();
        //  AppDbContext ArtistList();
        // bool Update(int id, string s, string r);
        // bool Delete(int id);
    }
}
