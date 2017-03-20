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
        #region CRUD Tests
        [Test]
        public void AddSingleBookmarkTest()
        {
            HistoryItemCollection bookmarkCollection = new HistoryItemCollection();
            HistoryItem demoBookmark = new HistoryItem();

            bookmarkCollection.Add(demoBookmark);

            Assert.AreEqual(1, bookmarkCollection.HistoryItems.Count);
        }

        [Test]
        public void AddNullBookmarkTest()
        {
            Assert.Catch(typeof(ArgumentNullException), AddNullBookmarktestDelegate);
        }
        
        private void AddNullBookmarktestDelegate()
        {
            HistoryItemCollection bookmarkCollection = new HistoryItemCollection();

            bookmarkCollection.Add(null);
        }
        #endregion

        #region Null Tests
        [Test]
        public void CheckBookmarksInitialized()
        {
            HistoryItemCollection bookmarkCollection = new HistoryItemCollection();

            Assert.NotNull(bookmarkCollection.HistoryItems);
        }

        [Test]
        public void CheckTimestampInitialized()
        {
            HistoryItemCollection bookmarkCollection = new HistoryItemCollection();

            Assert.NotNull(bookmarkCollection.TimeStamp);
        }
        #endregion

        #region Boundaries Tests
        [Test]
        public void CheckTimestampNotMinimum()
        {
            HistoryItemCollection bookmarkCollection = new HistoryItemCollection();

            Assert.AreNotEqual(DateTime.MinValue, bookmarkCollection.TimeStamp);
        }

        [Test]
        public void CheckTimestampNotMaximum()
        {
            HistoryItemCollection bookmarkCollection = new HistoryItemCollection();

            Assert.AreNotEqual(DateTime.MaxValue, bookmarkCollection.TimeStamp);
        }
        #endregion
    }
}
