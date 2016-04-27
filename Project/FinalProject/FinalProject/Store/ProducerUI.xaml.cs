using System.Windows;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for ProducerUI.xaml
    /// </summary>
    public partial class ProducerUi : Window
    {
        private readonly BookProducer _producer;
        public ProducerUi(BookProducer p)
        {
            InitializeComponent();
            _producer = p;
        }

        private void AddToStoreButton_Click(object sender, RoutedEventArgs e)
        {
            _producer.AddBookToStore();
        }
    }
}
