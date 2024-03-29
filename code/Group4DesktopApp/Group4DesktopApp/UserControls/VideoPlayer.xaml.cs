using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Group4DesktopApp.UserControls
{
    /// <summary>
    /// Interaction logic for VideoPlayer.xaml
    /// </summary>
    public partial class VideoPlayer : UserControl
    {
        private bool IsPlaying = false;
        private bool IsUserDraggingSlider = false;

        private readonly DispatcherTimer Timer = new() { Interval = TimeSpan.FromSeconds(0.1) };
        /// <summary>
        /// Initializes a new instance of the <see cref="VideoPlayer"/> class.
        /// </summary>
        public VideoPlayer()
        {
            InitializeComponent();
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        /// <summary>
        /// Loads the video.
        /// </summary>
        public void loadVideo()
        {
            OpenFileDialog MediaOpenDialog = new()
            {
                Title = "Open a media file",
                Filter = "Media Files (*.mp4, *.wmv, *.mpeg, *.avi)|*.mp4;*.wmv;*.mpeg;*.avi"
            };
            if (MediaOpenDialog.ShowDialog() == true)
            {
                this.videoFrame.LoadedBehavior = MediaState.Pause;
                this.videoFrame.Source = new Uri(MediaOpenDialog.FileName);
                this.IsPlaying = false;
            }

        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (this.videoFrame.Source != null && this.videoFrame.NaturalDuration.HasTimeSpan && !IsUserDraggingSlider)
            {
                this.progressSlider.Maximum = this.videoFrame.NaturalDuration.TimeSpan.TotalSeconds;
                this.progressSlider.Value = this.videoFrame.Position.TotalSeconds;
            }
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (this.videoFrame?.Source != null)
            {
                this.videoFrame.LoadedBehavior = MediaState.Play;
                this.IsPlaying = true;
            }
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsPlaying)
                this.videoFrame.LoadedBehavior = MediaState.Pause;
        }

        private void Slider_DragStarted(object sender, DragStartedEventArgs e)
        {
            this.IsUserDraggingSlider = true;
        }

        private void Slider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            this.IsUserDraggingSlider = false;
            this.videoFrame.Position = TimeSpan.FromSeconds(this.progressSlider.Value);
        }

        private void progressSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.lblTimePosition.Content = TimeSpan.FromSeconds(this.progressSlider.Value).ToString(@"hh\:mm\:ss");
        }
    }
}
