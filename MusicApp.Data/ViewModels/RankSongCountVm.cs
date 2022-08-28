using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Data.ViewModels
{
    public class RankSongCountVm
    {
        public int SongID { get; set; }
        public int ArtistID { get; set; }

        public string ArtistName { get; set; }
        public int SongCount { get; set; }

        public int Rank { get; set; }
    }
}
