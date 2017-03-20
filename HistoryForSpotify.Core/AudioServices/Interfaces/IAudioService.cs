using HistoryForSpotify.Commons.Models;
using HistoryForSpotify.Core.AudioServices.Delegates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryForSpotify.Core.AudioServices.Interfaces
{
    public interface IAudioService
    {
        event NewHistoryItemDelegate OnNewHistoryItem;
        event NewHistoryItemTrackTimeDelegate OnNewHistoryItemTrackTime;
        event ServiceConnectedDelegate OnServiceConnected;
        event ServiceDisconnectedDelegate OnServiceDisconnected;

        string Name { get; }
        void Connect();
        void Disconnect();
        void PlayHistoryItemFromPosition(HistoryItem itemToPlay);
        HistoryItem GetCurrentHistoryItem();
    }
}
