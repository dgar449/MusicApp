using System.ComponentModel.DataAnnotations.Schema;
namespace MusicApp.Controllers
{
    public class Album
    {
        public int AlbumID { get; set; }
        public string? AlbumName { get; set; }
        public string? ReleaseDate { get; set; }
        public string? Length { get; set; }       
        public int ArtistID { get; set; }

        [ForeignKey("ArtistID")]
        public Artist Artist { get; set; }

    }
}
