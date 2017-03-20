using HistoryForSpotify.Tests.Mockups.AudioService;
using HistoryForSpotify.Tests.Mockups.HistoryItemPersister;
using HistoryForSpotify.Tests.Mockups.Log;
using HistoryForSpotify.ViewModels;
using HistoryForSpotify.ViewModels.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryForSpotify.Tests.ViewModels
{
    [TestFixture]
    public class HistoryListViewModelsTest
    {
        [Test]
        public void LogNullExceptionTest()
        {


            Assert.Throws<ArgumentNullException>(() =>
            {
                IHistoryListViewModel _historyListViewModel = new HistoryListViewModel(null,
                                                                        new DefaultMockedAudioService(),
                                                                        new DefaultMockedHistoryItemPersister(), "");
            });
        }

        [Test]
        public void AudioServiceNullExceptionTest()
        {

        }

        [Test]
        public void HistoryItemPersisterNullExceptionTest()
        {

        }
    }
}
