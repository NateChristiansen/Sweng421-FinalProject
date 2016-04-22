using System.Windows;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        static readonly Store Store = new Store();
        BookProducer _prod = new BookProducer(Store);
        Client _client = new Client(Store);
    }
}
