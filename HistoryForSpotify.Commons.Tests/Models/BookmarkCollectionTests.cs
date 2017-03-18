using HistoryForSpotify.Commons.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryForSpotify.Commons.Tests.Models
{
    [TestFixture]
    public class BookmarkCollectionTests
    {
        [Test]
        public void AddSingleBookmarkTest()
        {
            BookmarkCollection bookmarkCollection = new BookmarkCollection();
            Bookmark demoBookmark = new Bookmark();

            bookmarkCollection.Add(demoBookmark);

            Assert.AreEqual(1, bookmarkCollection.Bookmarks.Count);
        }

        [Test]
        public void AddNullBookmarkTest()
        {
            Assert.Catch(typeof(ArgumentNullException), AddNullBookmarktestDelegate);
        }

        private void AddNullBookmarktestDelegate()
        {
            BookmarkCollection bookmarkCollection = new BookmarkCollection();

            bookmarkCollection.Add(null);
        }
    }
}
