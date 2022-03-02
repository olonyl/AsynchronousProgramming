using System;
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
            var result = Task.Run(() => LoginToTestDeadLock()).Result;

        }

        private async Task<string> LoginToTestDeadLock()
        {
            await Task.Delay(2000);

            return "Login Successful!";
        }

        private async Task LoginAsync()
        {
            try
            {
                btnLogin.IsEnabled = false;
                var result = await ContinuationUsingAyncAndAwait();
                btnLogin.Content = result;
                btnLogin.IsEnabled = true;
            }
            catch (Exception ex)
            {
                btnLogin.Content = "Internal error";
                btnLogin.IsEnabled = false;
            }
        }

        private async Task<string> ContinuationUsingAyncAndAwait()
        {
            //throw new Exception();
            try
            {
                var loginTask = Task.Run(async () =>
               {
                   await Task.Delay(2000);
                   return "Login Successful!";
               });

                return await loginTask;

            }
            catch (Exception)
            {
                return "Login Failed!";
            }
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
