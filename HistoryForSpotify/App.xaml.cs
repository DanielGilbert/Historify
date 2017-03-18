using HistoryForSpotify.Commons.Logging.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HistoryForSpotify
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ILog Log;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //Initialize Logger;

        }
    }
}
