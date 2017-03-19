using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryForSpotify.Commons.Models
{
    public class HistoryItemCollection
    {
        public DateTime TimeStamp { get; }
        public List<HistoryItem> HistoryItems { get; }

        public HistoryItemCollection()
        {
            HistoryItems = new List<HistoryItem>();
            TimeStamp = DateTime.UtcNow;
        }

        public void Add(HistoryItem bookmark)
        {
            if (HistoryItems == null)
                throw new InvalidOperationException("HistoryItems are not initialized");

            if (bookmark == null)
                throw new ArgumentNullException(nameof(bookmark));

            HistoryItems.Add(bookmark);
        }

        public void Delete(HistoryItem bookmark)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Insert(HistoryItem bookmark, int index)
        {
            throw new NotImplementedException();
        }
    }
}
