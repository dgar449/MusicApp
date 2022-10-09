using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Data.ViewModels
{
    public class GenrePopularityVm
    {
        public int GenreID { get; set; }
        public string? GenreType { get; set; }
        public int SongID { get; set; }
        public int Rank { get; set; }
        public int GenreCount { get; set; }

    }
}
