using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            this._client = client;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            _client.Login(this);
            this.Close();
        }

        private void LogRegWin_OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            _client.Register(this);
            this.Close();
        }
    }
}
