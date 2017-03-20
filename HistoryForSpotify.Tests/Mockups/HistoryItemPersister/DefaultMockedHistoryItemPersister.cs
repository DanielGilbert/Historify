using HistoryForSpotify.Core.Storage.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HistoryForSpotify.Commons.Models;

namespace HistoryForSpotify.Tests.Mockups.HistoryItemPersister
{
    class DefaultMockedHistoryItemPersister : IHistoryItemPersister
    {
        string _data;

        public List<HistoryItem> Load(string saveFolder)
        {
            return null;
        }

        /// <summary>
        /// "Saves" the dummy items
        /// </summary>
        /// <param name="historyItems"></param>
        /// <param name="saveFolder"></param>
        public void Save(List<HistoryItem> historyItems, string saveFolder)
        {

        }
    }
}
