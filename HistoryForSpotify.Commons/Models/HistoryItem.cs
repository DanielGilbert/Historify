using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace HistoryForSpotify.Commons.Models
{
    public class HistoryItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public byte[] AlbumArt { get; set; }
        public string AlbumArtUrl { get; set; }
        public Uri TrackUri { get; set; }
        public Uri AlbumUri { get; set; }
        public Uri ArtistUri { get; set; }
        public double CurrentPosition { get; set; }
        public double TrackLength { get; set; }
        public bool IsCompleted { get; set; }

        public HistoryItem()
        {
            Id = Guid.NewGuid();
        }
    }
}
