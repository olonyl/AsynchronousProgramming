using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace TaskParallelLibrary.Login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            ContinuationUsingConfigureAwait();
        }

        public void ContinuationUsingConfigureAwait()
        {
            btnLogin.IsEnabled = false;
            var task = Task.Run(() =>
            {
                // throw new System.Exception();
                Thread.Sleep(2000);
                return "Loign Successful!";
            });

            task.ConfigureAwait(true)
                .GetAwaiter()
                .OnCompleted(() =>
                {
                    btnLogin.Content = task.Result;
                    btnLogin.IsEnabled = true;
                });
        }

        public void ContinuationUsingContinueWith()
        {
            btnLogin.IsEnabled = false;

            var task = Task.Run(() =>
            {
                // throw new System.Exception();
                Thread.Sleep(2000);
                return "Loign Successful!";
            });

            task.ContinueWith((t) =>
            {

                Dispatcher.Invoke(() =>
                {
                    if (t.IsFaulted)
                    {
                        btnLogin.Content = "Login Failed";
                    }
                    else
                    {
                        btnLogin.Content = t.Result;
                    }
                    btnLogin.IsEnabled = true;
                });
            });
        }
    }
}
