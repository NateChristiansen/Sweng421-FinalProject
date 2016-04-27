using System;
using System.Collections.Generic;
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
    /// Interaction logic for ClientUI.xaml
    /// </summary>
    public partial class Client : Window
    {
        private readonly Store _store;
        public Client(Store s)
        {
            InitializeComponent();
            _store = s;
            PopulateStore();
            Show();
        }

        private void TabItemMouseDown(object sender, MouseButtonEventArgs e)
        {
            var text = (sender as Label).Content.ToString();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var query = SearchBox.Text;
            SearchResultList.Items.Clear();
            _store.Search(query).ForEach(b => SearchResultList.Items.Add(b.GetTitle()));
        }

        private void GoToStoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchResultList.SelectedItems.Count == 0) return;
            var x = SearchResultList.SelectedItems[0] as string;

        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PopulateStore()
        {
            _store.GetBooks().ForEach(b => BrowseGrid.Items.Add(b.GetTitle()));
        }
    }
}
