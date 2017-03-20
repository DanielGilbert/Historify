using HistoryForSpotify.Core.AudioServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HistoryForSpotify.Core.AudioServices.Delegates;
using HistoryForSpotify.Commons.Models;

namespace HistoryForSpotify.Tests.Mockups.AudioService
{
    public class DefaultMockedAudioService : IAudioService
    {
        public string Name
        {
            get
            {
                return "Default Mocked AudioService";
            }
        }

        public event NewHistoryItemDelegate OnNewHistoryItem = delegate { };
        public event NewHistoryItemTrackTimeDelegate OnNewHistoryItemTrackTime = delegate { };
        public event ServiceConnectedDelegate OnServiceConnected = delegate { };
        public event ServiceDisconnectedDelegate OnServiceDisconnected = delegate { };

        public void Connect()
        {
            OnServiceConnected();
        }

        public void Disconnect()
        {
            OnServiceDisconnected();
        }

        public HistoryItem GetCurrentHistoryItem()
        {
            HistoryItem dummyHistoryItem = new HistoryItem();

            dummyHistoryItem.Album = "Dummy";
            dummyHistoryItem.Artist = "Crashtest Dummies";
            dummyHistoryItem.Name = "Dummy Song";
            dummyHistoryItem.Id = new Guid("CA67CFF2-9374-4A95-9304-AEDE730B8337");
            dummyHistoryItem.TrackLength = 320d;
            dummyHistoryItem.CurrentPosition = 0d;

            return dummyHistoryItem;
        }

        public void PlayHistoryItemFromPosition(HistoryItem itemToPlay)
        {

        }
    }
}
