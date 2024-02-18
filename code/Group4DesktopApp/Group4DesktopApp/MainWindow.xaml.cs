using Group4DesktopApp.DAL;
using Group4DesktopApp.UserControls;
using Microsoft.Web.WebView2.WinForms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Group4DesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UIElement[] screens;
        private const double YOUTUBE_PLAYER_WIDTH_OFFSET = 20.0;
        private const double YOUTUBE_PLAYER_HEIGHT_OFFSET = 20.0;
        Regex youtubeRegex = new Regex("youtu(?:\\.be|be\\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)");
        public MainWindow()
        {
            InitializeComponent();
            this.screens = new UIElement[] { this.pdfViewer, this.videoPlayer, this.youtubePlayer };
        }

        private void showScreen(UIElement screen)
        {
            for (int i = 0; i < this.screens.Length; i++)
            {
                var screenElement = this.screens[i];
                if(screenElement == screen)
                {
                    screenElement.Visibility = Visibility.Visible;
                } else
                {
                    screenElement.Visibility = Visibility.Collapsed;
                    if(screenElement is VideoPlayer)
                    {
                        VideoPlayer vp = (VideoPlayer)screenElement;
                        vp.videoFrame.LoadedBehavior = MediaState.Stop;
                    }
                }
            }
        }

        private void btnOpenPDF_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".pdf";
            dialog.Filter = "PDF Documents (.pdf)|*.pdf";
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                this.showScreen(this.pdfViewer);
                //this.pdfViewer.Navigate(dialog.FileName);
                //Debug.WriteLine(AccountDAL.GetAccountID("Jeffrey353", "school"));
                var itms = SourceDAL.GetAllSourcesByUserId(1);
                //Stream stream = new MemoryStream(itms[0].Content);
                String code = System.Text.Encoding.Unicode.GetString(itms[0].Content);
                string extension = "pdf"; // "pdf", etc

                string filename = System.IO.Path.GetTempFileName() + "." + extension; // Makes something like "C:\Temp\blah.tmp.pdf"

                File.WriteAllBytes(filename, itms[0].Content);

                //this.pdfViewer.NavigateToString(code);
                this.pdfViewer.Navigate(filename);
                //this.pdfViewer.NavigateToStream
                   
            }
        }

        private void btnOpenVideo_Click(object sender, RoutedEventArgs e)
        {
            this.videoPlayer.loadVideo();
            this.showScreen(this.videoPlayer);
        }

        private async void loadYoutubeHTMLContent(string youtubeID)
        {
            await this.youtubePlayer.EnsureCoreWebView2Async(null);
            string htmlContent = @"
            <html>
            <head>
            <!-- Include the YouTube iframe API script using HTTPS -->
            <script src='https://www.youtube.com/iframe_api'></script>
            </head>
            <body>
            <!-- Embed the YouTube video with enablejsapi parameter over HTTPS -->
            <iframe id='player' type='text/html'";
            htmlContent += $"width='{this.youtubePlayer.Width - YOUTUBE_PLAYER_WIDTH_OFFSET}' " +
                $"height='{this.youtubePlayer.Height - YOUTUBE_PLAYER_HEIGHT_OFFSET}'";
            htmlContent += "src ='";
            htmlContent += $"https://www.youtube.com/embed/{youtubeID}?enablejsapi=1";
            htmlContent += @"'
                frameborder='0' allow='fullscreen';></iframe>

            <!-- JavaScript code to handle fullscreen changes -->
            <script>
             // Initialize the YouTube iframe API when the script is loaded
            function onYouTubeIframeAPIReady() {
            player = new YT.Player('player', {
            events: {
                    'onReady': onPlayerReady,
                    'onStateChange': onPlayerStateChange
                    }
                });
            }

            function onPlayerReady(event) {
            // Player is ready
            // You can control playback and perform other actions here
            }

            function onPlayerStateChange(event) {
            // Player state has changed (e.g., video started, paused, etc.)
            // Check if the player is in fullscreen mode
                var isFullscreen = document.fullscreenElement === player.getIframe();

                if (isFullscreen) {
            // Trigger the player's native fullscreen mode
                external.RequestFullscreen();
                } else {
            // Exit fullscreen
                external.ExitFullscreen();
             }
            }

            document.addEventListener('fullscreenchange', function () {
            console.log('Fullscreen change event triggered');
            window.chrome.webview.postMessage('fullscreenchange');
            });
            </script>
            </body>
            </html>
        ";
            this.youtubePlayer.NavigateToString(htmlContent);
        }

        private void btnYoutubePlay_Click(object sender, RoutedEventArgs e)
        {

            string? youtubeId = this.extractYoutubeLinkID(this.txtBoxLink.Text);
            if (youtubeId != null) {
            this.showScreen(this.youtubePlayer);
            this.loadYoutubeHTMLContent(youtubeId);
            }
        }

        private string? extractYoutubeLinkID(string url)
        {

            Match youtubeMatch = this.youtubeRegex.Match(url);

            string id = string.Empty;

            if (youtubeMatch.Success)
            {
                id = youtubeMatch.Groups[1].Value;
                return id;
            }
            return null;


        }

        private void youtubePlayer_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(this.youtubePlayer.Visibility != Visibility.Visible)
            {
                this.youtubePlayer.NavigateToString("");
            }
        }
    }
}
