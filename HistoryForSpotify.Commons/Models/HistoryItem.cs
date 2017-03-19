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
        public event Action<BitmapImage> OnAlbumArtLoaded = delegate { };

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public BitmapImage AlbumArt { get; set; }
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

        public void DownloadAlbumBitmapAsync()
        {
            Task.Factory.StartNew(() =>
            {
                using (WebClient wc = new WebClient())
                {

                    wc.Proxy = null;

                    if (String.IsNullOrWhiteSpace(AlbumArtUrl))
                        return;
                    Task<byte[]> task = wc.DownloadDataTaskAsync(AlbumArtUrl);
                    task.Wait();

                    using (MemoryStream ms = new MemoryStream(task.Result))
                    {
                        AlbumArt = BitmapToImageSource((Bitmap)Image.FromStream(ms));
                        OnAlbumArtLoaded(AlbumArt);
                    }
                }
            });

        }

        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            BitmapImage bmImg = new BitmapImage();

            using (MemoryStream memStream2 = new MemoryStream())
            {
                bitmap.Save(memStream2, System.Drawing.Imaging.ImageFormat.Png);

                bmImg.BeginInit();
                bmImg.CacheOption = BitmapCacheOption.OnLoad;
                bmImg.UriSource = null;
                bmImg.StreamSource = memStream2;
                bmImg.EndInit();
                bmImg.Freeze();
            }

            return bmImg;
        }
    }
}
