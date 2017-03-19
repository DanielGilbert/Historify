using HistoryForSpotify.Core.Storage.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HistoryForSpotify.Commons.Models;
using Newtonsoft.Json;
using System.IO;

namespace HistoryForSpotify.Core.Storage
{
    public class HistoryItemJsonPersister : IHistoryItemPersister
    {
        public List<HistoryItem> Load(string saveFolder)
        {
            string filename = "history.json";

            string data = File.ReadAllText(Path.Combine(saveFolder, filename));

            List<HistoryItem> items = JsonConvert.DeserializeObject<List<HistoryItem>>(data);

            return items;
        }

        public void Save(List<HistoryItem> historyItems, string saveFolder)
        {
            if (historyItems == null) return;

            string filename = "history.json";

            string data = JsonConvert.SerializeObject(historyItems);

            File.WriteAllText(Path.Combine(saveFolder, filename), data);
        }
    }
}
