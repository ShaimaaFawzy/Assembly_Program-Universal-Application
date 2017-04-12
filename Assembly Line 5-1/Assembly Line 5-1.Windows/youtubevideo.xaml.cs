using Assembly_Line_5_1.Common;
using MyToolkit.Multimedia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Assembly_Line_5_1
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class youtubevideo : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public youtubevideo()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        private void YouTubePlayer_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            // YouTubePlayer.IsFullWindow= YouTubePlayer. ;
            if (YouTubePlayerMediaElement.IsFullWindow == true)
            {
                YouTubePlayerMediaElement.IsFullWindow = false;
                Expand.Icon = new SymbolIcon(Symbol.FullScreen);
            }
            else
            {
                YouTubePlayerMediaElement.IsFullWindow = true;
                Expand.Icon = new SymbolIcon(Symbol.BackToWindow);

            }
        }
        private void YouTubePlayer_DoubleTapped(object sender, RoutedEventArgs e)
        {
            // YouTubePlayer.IsFullWindow= YouTubePlayer. ;
            if (YouTubePlayerMediaElement.IsFullWindow == true)
            {
                YouTubePlayerMediaElement.IsFullWindow = false;
                Expand.Icon = new SymbolIcon(Symbol.FullScreen);
            }
            else
            {
                YouTubePlayerMediaElement.IsFullWindow = true;
                Expand.Label = "normal screan";
                Expand.Icon = new SymbolIcon(Symbol.BackToWindow);

            }
        }

      
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }
        private void OnMediaOpened(object sender, RoutedEventArgs e)
        {
            ProgressProgressBar.IsEnabled = false;
            ProgressProgressBar.Visibility = Visibility.Collapsed;
        }

        private void OnMediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            ProgressProgressBar.IsEnabled = false;
        }

        private void Previousbtn_Click(object sender, RoutedEventArgs e)
        {
            //YouTubePlayer.DefaultPlaybackRate = -2.0;
            //YouTubePlayer.Play();

            YouTubePlayerMediaElement.Position -= TimeSpan.FromMilliseconds(800);
        }

        private void Pausebtn_Click(object sender, RoutedEventArgs e)
        {
            if (YouTubePlayerMediaElement.CurrentState == MediaElementState.Playing)
            {
                YouTubePlayerMediaElement.Pause();
                Pausebtn.Label = "play";
                Pausebtn.Icon = new SymbolIcon(Symbol.Play);

            }
            else if (YouTubePlayerMediaElement.CurrentState == MediaElementState.Paused)
            {
                Pausebtn.Icon = new SymbolIcon(Symbol.Pause);
                Pausebtn.Label = "pause";
                YouTubePlayerMediaElement.Play();
            }

        }

        private void Seekbtn_Click(object sender, RoutedEventArgs e)
        {
            //YouTubePlayer.DefaultPlaybackRate = 2.0;
            //YouTubePlayer.Play();

            YouTubePlayerMediaElement.Position += TimeSpan.FromMilliseconds(800);
        }
      
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
            YoutubeVideo video = e.Parameter as YoutubeVideo;
            if (video != null)
            {
                try
                {
                    if (NetworkInterface.GetIsNetworkAvailable())
                    {

                        string videoId = String.Empty;
                        if (video != null && !video.Id.Equals(String.Empty))
                        {
                            //Get The Video Uri and set it as a player source
                            var url = await YouTube.GetVideoUriAsync(video.Id, YouTubeQuality.Quality480P);
                             YouTubePlayerMediaElement.Source = url.Uri;
                            
                        }
                    }
                    else
                    {
                        MessageDialog message = new MessageDialog("You're not connected to Internet!");
                        await message.ShowAsync();
                        this.Frame.GoBack();
                    }
                }
                catch { }
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}
