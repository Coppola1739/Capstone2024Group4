using Group4DesktopApp.UserControls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
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

namespace Group4DesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UIElement[] screens;
        public MainWindow()
        {
            InitializeComponent();
            this.screens = new UIElement[] { this.pdfViewer, this.videoPlayer };
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
                this.pdfViewer.Navigate(dialog.FileName);
            }
        }

        private void btnOpenVideo_Click(object sender, RoutedEventArgs e)
        {
            this.videoPlayer.loadVideo();
            this.showScreen(this.videoPlayer);
        }
    }
}
