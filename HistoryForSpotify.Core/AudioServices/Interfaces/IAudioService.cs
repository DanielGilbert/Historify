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
        event NewTrackDelegate OnNewSong;
        event NewTrackTimeDelegate OnNewTrackTime;
        event ServiceConnectedDelegate OnServiceConnected;
        event ServiceDisconnectedDelegate OnServiceDisconnected;

        string Name { get; }
        void Connect();
        void Disconnect();
    }
}
