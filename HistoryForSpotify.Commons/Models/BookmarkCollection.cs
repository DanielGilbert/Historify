using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryForSpotify.Commons.Models
{
    public class BookmarkCollection
    {
        public DateTime TimeStamp { get; }
        public List<Bookmark> Bookmarks { get; }

        public BookmarkCollection()
        {
            Bookmarks = new List<Bookmark>();
        }

        public void Add(Bookmark bookmark)
        {
            if (Bookmarks == null)
                throw new InvalidOperationException("Bookmarks are not initialized");

            Bookmarks.Add(bookmark);
        }

        public void Delete(Bookmark bookmark)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Bookmark bookmark, int index)
        {
            throw new NotImplementedException();
        }
    }
}
