using System.ComponentModel.DataAnnotations.Schema;
namespace MusicApp.Controllers
{
    public class Song
    {
        public int SongID { get; set; }
        public string? SongTitle { get; set; }
        public int ArtistID { get; set; }
        
        public int AlbumID { get; set; }

        public int GenreID { get; set; }

        public DateTime? ReleaseDate { get; set; }
        public int TrackLength { get; set; }

        [ForeignKey("ArtistID")]
        public Artist Artist { get; set; }

        [ForeignKey("AlbumID")]
        public Album Album { get; set; }

        [ForeignKey("GenreID")]
        public Genre Genre { get; set; }


      
    }
}
