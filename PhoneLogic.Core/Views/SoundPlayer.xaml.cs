using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using PhoneLogic.Core.ViewModels;

namespace PhoneLogic.Core.Views
{
    public partial class SoundPlayer : UserControl
    {
        DebugEventsViewModel log;
       
        public SoundPlayer()
        {
            InitializeComponent();
            log = new DebugEventsViewModel();
            DataContext = log;
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += timer_Tick;
            media.AutoPlay = false;
            log.DebugEvents.Add("constructor");
            //media.Source = new Uri("2157788110_20090002_01_150246.wma", UriKind.Relative);
        }
        
        private void timer_Tick(object sender, EventArgs e)
        {
            lblStatus.Text = media.Position.ToString().TrimEnd(new char[] { '0' });
            sliderPositionBackground.Value = media.Position.TotalSeconds;
        }

        private DispatcherTimer timer = new DispatcherTimer();

        private void cmdPlay_Click(object sender, RoutedEventArgs e)
        {
            var fp = new Uri("/messages/2157788110_20090002_01_150246.wma", UriKind.Relative);
            MessageBox.Show(fp.ToString());
            media.Source = fp;
            media.Stop();
            media.Play();
            timer.Start();
        }
        private void cmdPause_Click(object sender, RoutedEventArgs e)
        {
            media.Play();
//            media.Pause();
            //timer.Stop();
        }
        private void cmdStop_Click(object sender, RoutedEventArgs e)
        {
            media.Stop();
            timer.Stop();
        }
        private void media_MediaOpened(object sender, RoutedEventArgs e)
        {
            sliderPosition.Maximum = media.NaturalDuration.TimeSpan.TotalSeconds;
        }
        private void sliderPosition_ValueChanged(object sender, RoutedEventArgs e)
        {
            media.Pause();
            media.Position = TimeSpan.FromSeconds(sliderPosition.Value);
            media.Play();
        }

        private void sliderBalance_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            media.Balance = sliderBalance.Value;
        }

        private void sliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (media != null) media.Volume = sliderVolume.Value;
        }

        private void chkMute_Click(object sender, RoutedEventArgs e)
        {
            media.IsMuted = (bool)chkMute.IsChecked;
        }

        private void media_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            lblStatus.Text = e.ErrorException.Message;
            log.DebugEvents.Add(e.ErrorException.Message);
        }

        private void media_MediaEnded(object sender, RoutedEventArgs e)
        {
            if ((bool)chkLoop.IsChecked)
            {
                media.Position = TimeSpan.Zero;
                media.Play();
            }
            else
            {
                timer.Stop();
            }
        }

        private void media_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            lblStatus.Text += media.CurrentState.ToString();
            log.DebugEvents.Add(media.CurrentState.ToString());
        }

    }
}
