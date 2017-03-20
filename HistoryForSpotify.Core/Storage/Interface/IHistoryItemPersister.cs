using HistoryForSpotify.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryForSpotify.Core.Storage.Interface
{
    public interface IHistoryItemPersister
    {
        void Save(List<HistoryItem> historyItems, string saveFolder);
        List<HistoryItem> Load(string saveFolder);
    }
}
