using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Data.ViewModels
{
    public class AlbumListVm
    {
        public int SongID { get; set; }
        public string? ArtistName { get; set; }
        public string? AlbumName { get; set; }
        public int AlbumID { get; set; }
        public int ArtistID { get; set; }
        public int Count { get; set; }

    }
}
