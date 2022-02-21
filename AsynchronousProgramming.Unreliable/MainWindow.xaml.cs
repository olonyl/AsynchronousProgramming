using System.Net;
using System.Threading;
using System.Windows;

namespace AsynchronousProgramming.Unreliable
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
            var client = new WebClient();

            var data = client.DownloadString("https://feeds.feedburner.com/fekberg");

            Thread.Sleep(10000);

            txtDownload.Text = data;
        }

        private void btnIncreaseCounter_Click(object sender, RoutedEventArgs e)
        {
            txtCounter.Text = $"Counter: {count++}";
        }
    }
}
