using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Microsoft.VisualBasic;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for ProducerUI.xaml
    /// </summary>
    public partial class Producer : Window
    {
        private readonly Store _store;
        private readonly List<Stock> _stocks = new List<Stock>();
        private readonly List<IBook> _unreleasedBooks = new List<IBook>(); 
        public Producer(Store s)
        {
            InitializeComponent();
            _store = s;
            InitUi();
            Show();
            InitBooks();
        }

        private void AddToStoreButton_Click(object sender, RoutedEventArgs e)
        {
            var book = (IBook)UnreleasedGrid.SelectedItem; // get the selected book
            var s = new Stock(book); // make new stock
            _stocks.Add(s); // add to stock list
            _unreleasedBooks.Remove(book); // remove book from the unreleased books list
            UpdateUnreleasedBookListBox(); // update the gui
            UpdateReleasedBookListBox();
            AddBookToStore(book); // add book to the store
        }

        private void InitUi()
        {
            UnreleasedGrid.Columns.Clear();
            var title = new DataGridTextColumn { Header = "Title", Binding = new Binding("Title"), IsReadOnly = true, Width = UnreleasedGrid.Width / 5 };
            var author = new DataGridTextColumn { Header = "Author", Binding = new Binding("Author"), IsReadOnly = true, Width = UnreleasedGrid.Width / 5 };
            var genre = new DataGridTextColumn { Header = "Genre", Binding = new Binding("Genre"), IsReadOnly = true, Width = UnreleasedGrid.Width / 5 };
            var publisher = new DataGridTextColumn { Header = "Publisher", Binding = new Binding("Publisher"), IsReadOnly = true, Width = UnreleasedGrid.Width / 5 };
            var price = new DataGridTextColumn { Header = "Price", Binding = new Binding("Price"), IsReadOnly = true, Width = UnreleasedGrid.Width / 5 };
            UnreleasedGrid.Columns.Add(title);
            UnreleasedGrid.Columns.Add(author);
            UnreleasedGrid.Columns.Add(genre);
            UnreleasedGrid.Columns.Add(publisher);
            UnreleasedGrid.Columns.Add(price);

            ReleasedGrid.Columns.Clear();
            var title1 = new DataGridTextColumn { Header = "Title", Binding = new Binding("Title"), IsReadOnly = true, Width = ReleasedGrid.Width / 5 };
            var author1 = new DataGridTextColumn { Header = "Author", Binding = new Binding("Author"), IsReadOnly = true, Width = ReleasedGrid.Width / 5 };
            var genre1 = new DataGridTextColumn { Header = "Genre", Binding = new Binding("Genre"), IsReadOnly = true, Width = ReleasedGrid.Width / 5 };
            var publisher1 = new DataGridTextColumn { Header = "Publisher", Binding = new Binding("Publisher"), IsReadOnly = true, Width = ReleasedGrid.Width / 5 };
            var price1 = new DataGridTextColumn { Header = "Price", Binding = new Binding("Price"), IsReadOnly = true, Width = ReleasedGrid.Width / 5 };
            ReleasedGrid.Columns.Add(title1);
            ReleasedGrid.Columns.Add(author1);
            ReleasedGrid.Columns.Add(genre1);
            ReleasedGrid.Columns.Add(publisher1);
            ReleasedGrid.Columns.Add(price1);
        }

        private void UpdateUnreleasedBookListBox()
        {
            UnreleasedGrid.ClearValue(ItemsControl.ItemsSourceProperty); // clear it
            UnreleasedGrid.ItemsSource = _unreleasedBooks;
        }

        private void UpdateReleasedBookListBox()
        {
            ReleasedGrid.ClearValue(ItemsControl.ItemsSourceProperty); // clear it
            ReleasedGrid.ItemsSource = _stocks.Select((stock => stock.GetBook()));
        }

        private void InitBooks()
        {
            // UNRELEASED BOOKS
            _unreleasedBooks.Add(new Book(
                new BitmapImage(),
                "Harry Potter",
                "Wizard boy wonder.",
                "J.K. Rowling",
                "Fantasy",
                "1234-51234",
                "Me.Awesome",
                new decimal(10))
                );

            _unreleasedBooks.Add(
                new Book(
                    new BitmapImage(),
                    "The Hunger Games",
                    "Kids battle to the death.",
                    "Suzanne Collins",
                    "Action",
                    "4321-62145",
                    "GoodPub",
                    new decimal(8))
                );

            _unreleasedBooks.Add(new Book(
                new BitmapImage(),
                "Paper Towns",
                "Sappy story",
                "John Green",
                "Drama",
                "3214-76582",
                "OneStory",
                new decimal(12.50))
                );

            // RELEASED BOOKS
            var s1 =
                new Stock(
                    new Book(new BitmapImage(), "The Martian",
                        "Man goes to space, grows potatoes, does well for himself", "Andy Weir", "Adventure",
                        "6543-98765", "Publishing Co.", new decimal(6.99)), 50);
            _stocks.Add(s1);

            var s2 =
                new Stock(
                    new Book(new BitmapImage(), "To Kill a Mockingbird", "Good book", "Harper Lee", "Drama",
                        "5576-09876", "NiceGuy", new decimal(10.00)), 50);
            _stocks.Add(s2);

            _stocks.ForEach(s => _store.AddBook(s));

            ReleasedGrid.ItemsSource = _stocks.Select((stock => stock.GetBook()));
            UnreleasedGrid.ItemsSource = _unreleasedBooks;
        }

        public void AddBookToStore(IBook book)
        {
            _store.AddBook(_stocks.First(s => s.GetBook().EqualsBook(book)));
        }

        private void UpdateQuantityButton_Click(object sender, RoutedEventArgs e)
        {
            // number to update the book by
            var updateNum = 0;

            // make sure a selection in the list has been made
            if (ReleasedGrid.SelectedIndex == -1) return;
            // grab the user's input
            var updateString = Interaction.InputBox("Please enter a number to update the selected book's quantity", 
                ((IBook)ReleasedGrid.SelectedItem).Title, "", -1, -1);
            try
            {
                // try using the user's input as the update number
                updateNum = int.Parse(updateString);
            }
            catch (Exception)
            {
                Console.WriteLine("Not a valid integer");
            }

            // get the current book
            var i = ReleasedGrid.SelectedIndex;

            if (updateNum > 0)
            {
                _stocks[i].UpdateQuanitity(updateNum);
            }
            else
            {
                Console.WriteLine("Update number was not > 0");
            }

            //Console.WriteLine(_stocks[i].GetQuantity());
        }

        private void UnreleasedSelected(object sender, SelectionChangedEventArgs e)
        {
            var b = (IBook) UnreleasedGrid.SelectedItem;
            if (b == null) return;
            UnreleasedDescription.Text = b.Summary;
            UnreleasedPreview.Source = b.Cover;
        }

        private void ReleasedSelected(object sender, SelectionChangedEventArgs e)
        {
            var b = (IBook)ReleasedGrid.SelectedItem;
            if (b == null) return;
            ReleasedDescription.Text = b.Summary;
            ReleasedPreview.Source = b.Cover;
        }
    }
}
