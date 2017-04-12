using Assembly_Line_5_1.Classes;
using Assembly_Line_5_1.Common;
using Microsoft.WindowsAzure.MobileServices;
using MyToolkit.Multimedia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Syndication;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Assembly_Line_5_1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public class dt
        {
            public string trgt { get; set; }
            public string titleid { get; set; }
        }
        //Azure Collection
        private MobileServiceCollection<AssApps, AssApps> WinItems;
        private IMobileServiceTable<AssApps> WinTable = App.MobileService.GetTable<AssApps>();
        private MobileServiceCollection<AssAppsWP, AssAppsWP> WpItems;
        private IMobileServiceTable<AssAppsWP> WPtable = App.MobileService.GetTable<AssAppsWP>();
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        public static int PublicId;
        public  MainPage()
        {
            this.InitializeComponent();
            bool Connect;
            Connect = IsInternet();
            if (Connect == true)
            {
                if (!CheckInternetConnectivity())
                    return;
            if (App.anim_ID == 0)
            { 
                start_anmi.Begin();
                start_anmi.Completed += start_anmi_Completed;
                App.anim_ID = 1;
            }
            else { }

            LogoAnim.Begin();

            allapps_Grid.Visibility = Visibility.Visible;
            topPhonApps_Grid.Visibility = Visibility.Collapsed;
            topStoreApps_Grid.Visibility = Visibility.Collapsed;
            ////news_Grid.Visibility = Visibility.Collapsed;
            blogs_grid.Visibility = Visibility.Collapsed;
            feedback_Grid.Visibility = Visibility.Collapsed;
            about_Grid.Visibility = Visibility.Collapsed;
            all_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 223, 55, 0));
            Mobile_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            lap_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            //news_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            blogs_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            feedback_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            about_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            Title_Text.Text = "All Apps";
            RefresAllApps();
            httpClient = new HttpClient();

            // Add a user-agent header
            var headers = httpClient.DefaultRequestHeaders;

            // HttpProductInfoHeaderValueCollection is a collection of 
            // HttpProductInfoHeaderValue items used for the user-agent header

            headers.UserAgent.ParseAdd("ie");
            headers.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");

         
            }
   else
            {

                 new MessageDialog("No Internet Connection , please check your connection and try again ", "Assembly Line 5-1 ").ShowAsync();
                 start_anmi.Begin();
            }
        }
        #region Internet connection
        public static bool CheckInternetConnectivity()
        {
            var internetProfile = NetworkInformation.GetInternetConnectionProfile();
            if (internetProfile == null)
                return false;

            return (internetProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);
        }

        public static bool IsInternet()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            return internet;
        }
        #endregion
        void start_anmi_Completed(object sender, object e)
        {
            end_start.Begin();
        }

       

        #region menu Event
        public int menu_id = 0;
        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (menu_id == 0)
            {
                Menu_Grid.Width = 243;
                menu_id = 1;
            }
            else if (menu_id == 1)
            {
                Menu_Grid.Width = 80;
                menu_id = 0;
            }
        }
        private void Menu_Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Menu_Grid.Width = 80;
            menu_id = 0;
        }

        private void Menu_Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Menu_Grid.Width = 243;
            menu_id = 1;
        }
        private void ListViewItem_Tapped_5(object sender, TappedRoutedEventArgs e)
        {
            allapps_Grid.Visibility = Visibility.Visible;
            topPhonApps_Grid.Visibility = Visibility.Collapsed;
            topStoreApps_Grid.Visibility = Visibility.Collapsed;
            //news_Grid.Visibility = Visibility.Collapsed;
            blogs_grid.Visibility = Visibility.Collapsed;
            feedback_Grid.Visibility = Visibility.Collapsed;
            about_Grid.Visibility = Visibility.Collapsed;
            all_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 223, 55, 0));
            Mobile_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            lap_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            //news_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            blogs_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            feedback_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            about_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            Title_Text.Text = "All Apps";
        }

        private void ListViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            allapps_Grid.Visibility = Visibility.Collapsed;
            topPhonApps_Grid.Visibility = Visibility.Visible;
            topStoreApps_Grid.Visibility = Visibility.Collapsed;
            //news_Grid.Visibility = Visibility.Collapsed;
            blogs_grid.Visibility = Visibility.Collapsed;
            feedback_Grid.Visibility = Visibility.Collapsed;
            about_Grid.Visibility = Visibility.Collapsed;
            all_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            Mobile_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 223, 55, 0));
            lap_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            //news_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            blogs_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            feedback_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            about_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            Title_Text.Text = "Top Windows Phone Apps";
            RefreshTopWP();
        }

        private void ListViewItem_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            allapps_Grid.Visibility = Visibility.Collapsed;
            topPhonApps_Grid.Visibility = Visibility.Collapsed;
            topStoreApps_Grid.Visibility = Visibility.Visible;
            //news_Grid.Visibility = Visibility.Collapsed;
            blogs_grid.Visibility = Visibility.Collapsed;
            feedback_Grid.Visibility = Visibility.Collapsed;
            about_Grid.Visibility = Visibility.Collapsed;
            all_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            Mobile_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            lap_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 223, 55, 0));
            //news_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            blogs_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            feedback_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            about_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));

            Title_Text.Text = "Top Windows Store Apps";
            RefreshTopWindowsApps();

        }

        private void ListViewItem_Tapped_2(object sender, TappedRoutedEventArgs e)
        {
            allapps_Grid.Visibility = Visibility.Collapsed;
            topPhonApps_Grid.Visibility = Visibility.Collapsed;
            topStoreApps_Grid.Visibility = Visibility.Collapsed;
            //news_Grid.Visibility = Visibility.Visible;
            blogs_grid.Visibility = Visibility.Collapsed;
            feedback_Grid.Visibility = Visibility.Collapsed;
            about_Grid.Visibility = Visibility.Collapsed;
            all_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            Mobile_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            lap_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            //news_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 223, 55, 0));
            blogs_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            feedback_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            about_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));

            Title_Text.Text = "Assembly News";
        }

        private async void ListViewItem_Tapped_3(object sender, TappedRoutedEventArgs e)
        {
            allapps_Grid.Visibility = Visibility.Collapsed;
            topPhonApps_Grid.Visibility = Visibility.Collapsed;
            topStoreApps_Grid.Visibility = Visibility.Collapsed;
            //news_Grid.Visibility = Visibility.Collapsed;
            blogs_grid.Visibility = Visibility.Visible;
            feedback_Grid.Visibility = Visibility.Collapsed;
            about_Grid.Visibility = Visibility.Collapsed;
            all_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            Mobile_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            lap_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            //news_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            blogs_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 223, 55, 0));
            feedback_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            about_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            Title_Text.Text = "Tutorials";
            response = new HttpResponseMessage();

            // if 'feedAddress' value changed the new URI must be tested --------------------------------
            // if the new 'feedAddress' doesn't work, 'feedStatus' informs the user about the incorrect input.

            //feedStatus.Text = "Test if URI is valid";
            progressRing1.Visibility = Visibility.Visible;
            youtubechanel();
            Uri resourceUri;
            if (!Uri.TryCreate(feedAddress.Trim(), UriKind.Absolute, out resourceUri))
            {
                progressRing1.Visibility = Visibility.Collapsed;
                feedStatus.Text = "Invalid URI, please re-enter a valid URI";
                feedStatus.FontSize = 40;
                return;
            }
            if (resourceUri.Scheme != "http" && resourceUri.Scheme != "https")
            {
                progressRing1.Visibility = Visibility.Collapsed;
                feedStatus.Text = "Only 'http' and 'https' schemes supported. Please re-enter URI";
                feedStatus.FontSize = 40;
                return;
            }
            // ---------- end of test---------------------------------------------------------------------

            string responseText;
            feedStatus.Text = "Waiting for response ...";

            try
            {
                response = await httpClient.GetAsync(resourceUri);

                response.EnsureSuccessStatusCode();

                responseText = await response.Content.ReadAsStringAsync();
                blogs_status.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                Tutorial_stack.Visibility = Windows.UI.Xaml.Visibility.Visible;

            }
            catch (Exception ex)
            {
                progressRing1.Visibility = Visibility.Collapsed;
                // Need to convert int HResult to hex string
                feedStatus.Text = "Error = " + ex.HResult.ToString("X") +
                    "  Message: " + ex.Message;
                responseText = "";
            }
            feedStatus.Text = response.StatusCode + " " + response.ReasonPhrase;
            // now 'responseText' contains the feed as a verified text.
            // next 'responseText' is converted as the rssItems class model definition to be displayed as a list
            List<rssItems> lstData = new List<rssItems>();
            XElement _xml = XElement.Parse(responseText);
            foreach (XElement value in _xml.Elements("channel").Elements("item"))
            {
                rssItems _item = new rssItems();

                _item.Title = value.Element("title").Value;

                _item.Description = value.Element("description").Value;

                _item.Link = value.Element("link").Value;
                lstData.Add(_item);
            }
            // lstRSS is bound to the lstData: the final result of the RSS parsing
            blogs_list.DataContext = lstData;
        }

        private void ListViewItem_Tapped_4(object sender, TappedRoutedEventArgs e)
        {
            allapps_Grid.Visibility = Visibility.Collapsed;
            topPhonApps_Grid.Visibility = Visibility.Collapsed;
            topStoreApps_Grid.Visibility = Visibility.Collapsed;
            //news_Grid.Visibility = Visibility.Collapsed;
            blogs_grid.Visibility = Visibility.Collapsed;
            feedback_Grid.Visibility = Visibility.Visible;
            about_Grid.Visibility = Visibility.Collapsed;
            all_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            Mobile_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            lap_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            //news_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            blogs_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            feedback_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 223, 55, 0));
            about_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));

            Title_Text.Text = "Feedback";
        }
        private void ListViewItem_Tapped_6(object sender, TappedRoutedEventArgs e)
        {
            allapps_Grid.Visibility = Visibility.Collapsed;
            topPhonApps_Grid.Visibility = Visibility.Collapsed;
            topStoreApps_Grid.Visibility = Visibility.Collapsed;
            //news_Grid.Visibility = Visibility.Collapsed;
            blogs_grid.Visibility = Visibility.Collapsed;
            feedback_Grid.Visibility = Visibility.Collapsed;
            about_Grid.Visibility = Visibility.Visible;
            all_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            Mobile_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            lap_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            //news_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            blogs_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            feedback_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            about_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 223, 55, 0));

            Title_Text.Text = "About assembly";
        }

        #endregion
       
        #region AllApps Grid Event

        private void Grid_all_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SolidColorBrush blue = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 32, 200, 255);

            SolidColorBrush white = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 255, 255, 255);

            rall.Fill = blue;
            rSocial.Fill = white;
            rEducation.Fill = white;
            rHealth.Fill = white;
            rTravel.Fill = white;
            rCommunity.Fill = white;
            rGame.Fill = white;
            RefresAllApps();

                                      
        }

        private void Grid_Social_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SolidColorBrush blue = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 32, 200, 255);

            SolidColorBrush white = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 255, 255, 255);

            rall.Fill = white;
            rSocial.Fill = blue;
            rEducation.Fill = white;
            rHealth.Fill = white;
            rTravel.Fill = white;
            rCommunity.Fill = white;
            rGame.Fill = white;
            RefreshSocialApps();
     
        }

        private void Grid_Education_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SolidColorBrush blue = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 32, 200, 255);

            SolidColorBrush white = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 255, 255, 255);

            rall.Fill = white;
            rSocial.Fill = white;
            rEducation.Fill = blue;
            rHealth.Fill = white;
            rTravel.Fill = white;
            rCommunity.Fill = white;
            rGame.Fill = white;
            RefreshEducationApps();

        }

        private void grid_Health_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SolidColorBrush blue = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 32, 200, 255);

            SolidColorBrush white = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 255, 255, 255);

            rall.Fill = white;
            rSocial.Fill = white;
            rEducation.Fill = white;
            rHealth.Fill = blue;
            rTravel.Fill = white;
            rCommunity.Fill = white;
            rGame.Fill = white;
            RefreshHealthApps();

        }

        private void Grid_Travel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SolidColorBrush blue = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 32, 200, 255);

            SolidColorBrush white = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 255, 255, 255);

            rall.Fill = white;
            rSocial.Fill = white;
            rEducation.Fill = white;
            rHealth.Fill = white;
            rTravel.Fill = blue;
            rCommunity.Fill = white;
            rGame.Fill = white;
            RefreshTravelApps();

        }

        private void Grid_Community_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SolidColorBrush blue = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 32, 200, 255);

            SolidColorBrush white = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 255, 255, 255);

            rall.Fill = white;
            rSocial.Fill = white;
            rEducation.Fill = white;
            rHealth.Fill = white;
            rTravel.Fill = white;
            rCommunity.Fill = blue;
            rGame.Fill = white;
            RefreshCommunityApps();
        }

        private void Grid_Game_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SolidColorBrush blue = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 32, 200, 255);

            SolidColorBrush white = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 255, 255, 255);

            rall.Fill = white;
            rSocial.Fill = white;
            rEducation.Fill = white;
            rHealth.Fill = white;
            rTravel.Fill = white;
            rCommunity.Fill = white;
            rGame.Fill = blue;
            RefreshGameApps();
        }
        #endregion

        private void allApps_gridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(Store_Details),e.ClickedItem);
        }

        private void TopWpApps_gridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(Phone_Details),e.ClickedItem);
        }

        private void TopStoreApps_gridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(Store_Details),e.ClickedItem);
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(News_Details));
        }

        private void StackPanel_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            face_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 59, 89, 152));
            face_text.Foreground = new SolidColorBrush(Color.FromArgb(255, 59, 89, 152));
        }

        private void StackPanel_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            face_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            face_text.Foreground = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
        }

        private void StackPanel_PointerExited_1(object sender, PointerRoutedEventArgs e)
        {
            twitter_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
            twitter_text.Foreground = new SolidColorBrush(Color.FromArgb(255, 141, 150, 155));
        }

        private void StackPanel_PointerEntered_1(object sender, PointerRoutedEventArgs e)
        {
            twitter_icon.Fill = new SolidColorBrush(Color.FromArgb(255, 85, 172, 238));
            twitter_text.Foreground = new SolidColorBrush(Color.FromArgb(255, 85, 172, 238));
        }

      

        private void Button_Click(object sender, TappedRoutedEventArgs e)
        {
            dt m = new dt();
            m.titleid = "Apply For Assembly Line 5-1";
            m.trgt = "http://1drv.ms/1CAsyq2";
            Uri targetUri = new Uri(@"http://1drv.ms/1CAsyq2");
            Frame.Navigate(typeof(web_view), m);
        }




        #region AllRefrashFunctions
        private async void RefreshTopWindowsApps()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {

                WinItems = await WinTable.Where(todoItem => todoItem.topapps == 1).ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Internet Problem").ShowAsync();
            }
            else
            {

                TopStoreApps_gridView.ItemsSource = WinItems;
            }
        }

        private async void RefreshTopWP()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {

                WpItems = await WPtable.Where(todoItem => todoItem.topapps == 1).ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Internet Problem").ShowAsync();
            }
            else
            {

                TopWpApps_gridView.ItemsSource = WpItems;
            }
        }
        private async void RefresAllApps()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {

                WinItems = await WinTable.Where(todoItem => todoItem.Complete == false).ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Internet Problem").ShowAsync();
            }
            else
            {

                allApps_gridView.ItemsSource = WinItems;
            }
        }
    
        private async void RefreshSocialApps()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {

                WinItems = await WinTable.Where(todoItem => todoItem.appsection == "Social").ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Internet Problem").ShowAsync();
            }
            else
            {

                allApps_gridView.ItemsSource = WinItems;
            }
        }
        private async void RefreshEducationApps()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {

                WinItems = await WinTable.Where(todoItem => todoItem.appsection == "Education").ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Internet Problem").ShowAsync();
            }
            else
            {

                allApps_gridView.ItemsSource = WinItems;
            }
        }
        private async void RefreshHealthApps()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {

                WinItems = await WinTable.Where(todoItem => todoItem.appsection == "Health").ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Internet Problem").ShowAsync();
            }
            else
            {

                allApps_gridView.ItemsSource = WinItems;
            }
        }
        private async void RefreshTravelApps()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {

                WinItems = await WinTable.Where(todoItem => todoItem.appsection == "Travel").ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Internet Problem").ShowAsync();
            }
            else
            {

                allApps_gridView.ItemsSource = WinItems;
            }
        }
        private async void RefreshCommunityApps()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {

                WinItems = await WinTable.Where(todoItem => todoItem.appsection == "Community").ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Internet Problem").ShowAsync();
            }
            else
            {

                allApps_gridView.ItemsSource = WinItems;
            }
        }
        private async void RefreshGameApps()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {

                WinItems = await WinTable.Where(todoItem => todoItem.appsection == "Game").ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Internet Problem").ShowAsync();
            }
            else
            {

                allApps_gridView.ItemsSource = WinItems;
            }
        }

        #endregion

        #region Wp Function 
        private async void RefresAllWPApps()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {

                WpItems = await WPtable.Where(todoItem => todoItem.Complete == false).ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Internet Problem").ShowAsync();
            }
            else
            {

                allWPApps_gridView.ItemsSource = WpItems;
            }
        }
        private async void RefreshWPSocialApps()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {

                WpItems = await WPtable.Where(todoItem => todoItem.appsection == "Social").ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Internet Problem").ShowAsync();
            }
            else
            {

                allWPApps_gridView.ItemsSource = WpItems;
            }
        }
        private async void RefreshWPEducationApps()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {

                WpItems = await WPtable.Where(todoItem => todoItem.appsection == "Education").ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Internet Problem").ShowAsync();
            }
            else
            {

                allWPApps_gridView.ItemsSource = WpItems;
            }
        }
        private async void RefreshWPHealthApps()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {

                WpItems = await WPtable.Where(todoItem => todoItem.appsection == "Health").ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Internet Problem").ShowAsync();
            }
            else
            {

                allWPApps_gridView.ItemsSource = WpItems;
            }
        }
        private async void RefreshWPTravelApps()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {

                WpItems = await WPtable.Where(todoItem => todoItem.appsection == "Travel").ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Internet Problem").ShowAsync();
            }
            else
            {

                allWPApps_gridView.ItemsSource = WpItems;
            }
        }
        private async void RefreshWPCommunityApps()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {

                WpItems = await WPtable.Where(todoItem => todoItem.appsection == "Community").ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Internet Problem").ShowAsync();
            }
            else
            {

                allWPApps_gridView.ItemsSource = WpItems;
            }
        }
        private async void RefreshWPGameApps()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {

                WpItems = await WPtable.Where(todoItem => todoItem.appsection == "Game").ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Internet Problem").ShowAsync();
            }
            else
            {

                allWPApps_gridView.ItemsSource = WpItems;
            }
        }


        #endregion
        #region RSS
        private HttpClient httpClient;
        private HttpResponseMessage response;

        // This is the feed address that will be parsed and displayed
        private String feedAddress = "https://egyptappfactory.wordpress.com/feed/";

        private  void lstRSS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // if item clicked navigate to the webpage

            var selected = blogs_list.SelectedItem as rssItems;

            //WebView webBrowserTask = new WebView();
            Uri targetUri = new Uri(selected.Link);
            dt n = new dt();
            n.trgt = selected.Link;
            n.titleid = "Technical Tutorials For More Learning";

            //webbrowser task launcher for Windows 8.1
            //http://msdn.microsoft.com/en-us/library/windows/apps/xaml/hh701480.aspx
            //var success = await Windows.System.Launcher.LaunchUriAsync(targetUri);

            Frame.Navigate(typeof(web_view), n);
        }

        #endregion

        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SolidColorBrush blue = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 32, 200, 255);

            SolidColorBrush white = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 255, 255, 255);

            phoneral.Fill = blue;
            storeral.Fill = white;
            winApps.Visibility = Visibility.Collapsed;
            WpApps.Visibility = Visibility.Visible;
            RefresAllWPApps();
            allApps_gridView.Visibility = Visibility.Collapsed;
            allWPApps_gridView.Visibility = Visibility.Visible;
            
        }

        private void store_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SolidColorBrush blue = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 32, 200, 255);

            SolidColorBrush white = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 255, 255, 255);

            phoneral.Fill = white;
            storeral.Fill = blue;
            winApps.Visibility = Visibility.Visible;
            WpApps.Visibility = Visibility.Collapsed;
            RefresAllApps();
            allApps_gridView.Visibility = Visibility.Visible;
            allWPApps_gridView.Visibility = Visibility.Collapsed;
            
        }

        private void StackPanel_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            var uri = new Uri("https://www.facebook.com/EGAppFactory");
            IAsyncOperation<bool> x = Windows.System.Launcher.LaunchUriAsync(uri);
            
        }

        private void StackPanel_Tapped_2(object sender, TappedRoutedEventArgs e)
        {
            var uri = new Uri("https://twitter.com/EGAppFactory");
            IAsyncOperation<bool> x = Windows.System.Launcher.LaunchUriAsync(uri);

        }

        private void Grid_allWP_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SolidColorBrush blue = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 32, 200, 255);

            SolidColorBrush white = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 255, 255, 255);

            rall2.Fill = blue;
            rSocial2.Fill = white;
            rEducation2.Fill = white;
            rHealth2.Fill = white;
            rTravel2.Fill = white;
            rCommunity2.Fill = white;
            rGame2.Fill = white;
            RefresAllWPApps();
        }

        private void Grid_WPSocial_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SolidColorBrush blue = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 32, 200, 255);

            SolidColorBrush white = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 255, 255, 255);

            rall2.Fill = blue;
            rSocial2.Fill = white;
            rEducation2.Fill = white;
            rHealth2.Fill = white;
            rTravel2.Fill = white;
            rCommunity2.Fill = white;
            rGame2.Fill = white;
            RefreshWPSocialApps();
        }

        private void Grid_wpEducation_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SolidColorBrush blue = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 32, 200, 255);

            SolidColorBrush white = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 255, 255, 255);

            rall2.Fill = blue;
            rSocial2.Fill = white;
            rEducation2.Fill = white;
            rHealth2.Fill = white;
            rTravel2.Fill = white;
            rCommunity2.Fill = white;
            rGame2.Fill = white;
            RefreshWPEducationApps();
        }

        private void grid_WPHealth_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SolidColorBrush blue = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 32, 200, 255);

            SolidColorBrush white = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 255, 255, 255);

            rall2.Fill = blue;
            rSocial2.Fill = white;
            rEducation2.Fill = white;
            rHealth2.Fill = white;
            rTravel2.Fill = white;
            rCommunity2.Fill = white;
            rGame2.Fill = white;
            RefreshWPHealthApps();
        }

        private void Grid_WPTravel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SolidColorBrush blue = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 32, 200, 255);

            SolidColorBrush white = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 255, 255, 255);

            rall2.Fill = blue;
            rSocial2.Fill = white;
            rEducation2.Fill = white;
            rHealth2.Fill = white;
            rTravel2.Fill = white;
            rCommunity2.Fill = white;
            rGame2.Fill = white;
            RefreshWPTravelApps();
        }

        private void Grid_WpCommunity_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SolidColorBrush blue = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 32, 200, 255);

            SolidColorBrush white = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 255, 255, 255);

            rall2.Fill = blue;
            rSocial2.Fill = white;
            rEducation2.Fill = white;
            rHealth2.Fill = white;
            rTravel2.Fill = white;
            rCommunity2.Fill = white;
            rGame2.Fill = white;
            RefreshWPCommunityApps();
        }

        private void Grid_WPGame_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SolidColorBrush blue = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 32, 200, 255);

            SolidColorBrush white = new SolidColorBrush();
            blue.Color = Color.FromArgb(255, 255, 255, 255);

            rall2.Fill = blue;
            rSocial2.Fill = white;
            rEducation2.Fill = white;
            rHealth2.Fill = white;
            rTravel2.Fill = white;
            rCommunity2.Fill = white;
            rGame2.Fill = white;
            RefreshWPGameApps();
        }

        private void allWPApps_gridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(Phone_Details), e.ClickedItem);
        }

        private async Task<List<YoutubeVideo>> GetYoutubeChannel(string url)
        {
            try
            {
                SyndicationClient client = new SyndicationClient();
                SyndicationFeed feed = await client.RetrieveFeedAsync(new Uri(url));

                List<YoutubeVideo> videosList = new List<YoutubeVideo>();
                YoutubeVideo video;
                foreach (SyndicationItem item in feed.Items)
                {
                    video = new YoutubeVideo();

                    video.YoutubeLink = item.Links[0].Uri;
                    string a = video.YoutubeLink.ToString().Remove(0, 31);
                    video.Id = a.Substring(0, 11);
                    video.Title = item.Title.Text;
                    video.PubDate = item.PublishedDate.DateTime;

                    video.Thumbnail = YouTube.GetThumbnailUri(video.Id, YouTubeThumbnailSize.Medium);

                    videosList.Add(video);
                }

                return videosList;
            }
            catch
            {
                return null;
            }
        }
        protected async void youtubechanel()
        {
            try
            {
                if (NetworkInterface.GetIsNetworkAvailable())
                {
                    //Choose the gap of the videos list
                    //Index
                    int index = 1;
                    //The number of videos you would like to get (from 1 to 50)
                    int maxResults = 50;

                    //To get more than 50 videos, just use pagination and change the index :
                    //int index = 51;

                    //Channel Videos
                    ChannelVideosListView.Visibility = Visibility.Collapsed;
                    //Here is the name of the Channel
                    string youtubeChannel = "UCKoSsX64YWkpXyYPmbBLZVg";
                    var channelVideos = await GetYoutubeChannel("http://gdata.youtube.com/feeds/base/users/" + youtubeChannel + "/uploads?alt=rss&v=2&orderby=published&start-index=" + index + "&max-results=" + maxResults);
                    ChannelVideosListView.ItemsSource = channelVideos;

                    ChannelVideosListView.Visibility = Visibility.Visible;


                }
                else
                {
                    MessageDialog message = new MessageDialog("You're not connected to Internet!");
                    await message.ShowAsync();
                }
            }
            catch { }
        }
        private async void ChannelVideosListViewItemClick(object sender, ItemClickEventArgs e)
        {
            YoutubeVideo video = e.ClickedItem as YoutubeVideo;
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
                            // YouTubePlayerMediaElement.Source = url.Uri;
                            this.Frame.Navigate(typeof(youtubevideo),e.ClickedItem);
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
      
 

    }
}
