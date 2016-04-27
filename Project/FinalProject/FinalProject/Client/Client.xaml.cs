using System;
using System.Collections.Generic;
using System.Dynamic;
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

        private void InitUi()
        {
        }

        private void TabItemMouseDown(object sender, MouseButtonEventArgs e)
        {
            var text = (sender as Label).Content.ToString();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var query = SearchBox.Text;
            SearchResultList.Items.Clear();
            _store.Search(query).ForEach(b => SearchResultList.Items.Add(b.Title));
        }

        private void GoToStoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchResultList.SelectedItems.Count == 0) return;
            var x = SearchResultList.SelectedItems[0] as string;

        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            PopulateStore();
        }

        private void PopulateStore()
        {
            BrowseGrid.Columns.Clear();
            var title = new DataGridTextColumn {Header = "Title", Binding = new Binding("Title")};
            var author = new DataGridTextColumn {Header = "Author", Binding = new Binding("Author")};
            var genre = new DataGridTextColumn {Header = "Genre", Binding = new Binding("Genre")};
            var publisher = new DataGridTextColumn {Header = "Publisher", Binding = new Binding("Publisher")};
            var price = new DataGridTextColumn {Header = "Price", Binding = new Binding("Price")};
            var books = _store.GetBooks();
            BrowseGrid.Columns.Add(title);
            BrowseGrid.Columns.Add(author);
            BrowseGrid.Columns.Add(genre);
            BrowseGrid.Columns.Add(publisher);
            BrowseGrid.Columns.Add(price);
            books.ForEach(b => BrowseGrid.Items.Add(b));
        }

        private void BrowseSelected(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            var selected = (IBook) selectionChangedEventArgs.AddedItems[0];
            BrowseDescriptionText.Text = selected.Summary;
            BrowseImage.Source = selected.Cover;
        }
    }
}
