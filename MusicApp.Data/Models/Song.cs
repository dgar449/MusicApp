namespace MusicApp.Controllers
{
    public class Song
    {
        public int SongID { get; set; }
        public string? SongTitle { get; set; }
        public int Artist { get; set; }
        public int AlbumID { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int TrackLength { get; set; }

        public virtual Actor Artist { get; set; }
    }

    public class Actor
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public virtual List<Song> Songs { get; set; }
    }
}
