using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Data.ViewModels
{
    public class MusicCareer
    {
        public int SongID { get; set; }
        public int ArtistID { get; set; }
        public string? ArtistName { get; set; }
        public DateTime? Oldest { get; set; }
        public DateTime ReleaseDate { get; set; }
        public TimeSpan CareerLength { get; set; }
        public double CareerLengthYears { get; set; }
        public DateTime? Newest { get; set; }    
        public double CareerDays { get; set; }  
        public int Rank { get; set; }
        public int SongCount { get; set; }
    }
}
