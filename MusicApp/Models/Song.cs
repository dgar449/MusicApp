namespace MusicApp.Controllers
{
    public class Song
    {
        public int SongID { get; set; }
        public string? SongTitle { get; set; }
        public int ArtitstID { get; set; }
        public int GenreID { get; set; }
        public int AlbumID { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Length { get; set; }
    }
}
