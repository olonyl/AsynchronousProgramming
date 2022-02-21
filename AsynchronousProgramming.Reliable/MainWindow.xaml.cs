using System;
using System.Net;
using System.Windows;

namespace AsynchronousProgramming.Reliable
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int count = 1;
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void btnDowload_Click(object sender, RoutedEventArgs e)
        {
            btnDowload.IsEnabled = false;

            var client = new WebClient();

            client.DownloadStringAsync(new Uri("https://feeds.feedburner.com/fekberg"));

            client.DownloadStringCompleted += Client_DownloadStringCompleted;
        }

        private void Client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            txtDownload.Text = e.Result;
            btnDowload.IsEnabled = true;
        }

        private void btnIncreaseCounter_Click(object sender, RoutedEventArgs e)
        {
            txtCounter.Text = $"Counter: {count++}";
        }
    }
}
