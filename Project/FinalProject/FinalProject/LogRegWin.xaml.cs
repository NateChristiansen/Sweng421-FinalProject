using System.ComponentModel;
using System.Windows;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for LogRegWin.xaml
    /// </summary>
    public partial class LogRegWin : Window
    {
        private Client _client;
        public LogRegWin(Client client)
        {
            InitializeComponent();
            _client = client;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            _client.Login(this);
            Close();
        }

        private void LogRegWin_OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            _client.Register(this);
            Close();
        }
    }
}
