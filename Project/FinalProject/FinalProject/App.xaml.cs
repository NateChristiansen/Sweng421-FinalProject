using System.Windows;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        static readonly Store Store = new Store();
        Producer _prod = new Producer(Store);
        Client _client = new Client(Store);
    }
}
