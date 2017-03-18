using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryForSpotify.Commons.Models
{
    public class Bookmark
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public Bitmap AlbumArt { get; set; }
        public Uri TrackUri { get; set; }
        public Uri AlbumUri { get; set; }
        public Uri ArtistUri { get; set; }
        public double CurrentPosition { get; set; }
        public double TrackLength { get; set; }
        public bool IsCompleted { get; set; }

        public Bookmark()
        {
            Id = Guid.NewGuid();
        }

    }
}
