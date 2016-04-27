using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            Show();
            InitBooks();
        }

        private void AddToStoreButton_Click(object sender, RoutedEventArgs e)
        {
            IBook Book = _unreleasedBooks[NotInStoreBox.SelectedIndex];
            _unreleasedBooks.Remove(Book);
            AddBookToStore(Book);
        }

        private void InitBooks()
        {
            // UNRELEASED BOOKS
            _unreleasedBooks.Add(
                new Book(
                    new Image(),
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
                    new Image(),
                    "The Hunger Games",
                    "Kids battle to the death.",
                    "Suzanne Collins",
                    "Action",
                    "4321-62145",
                    "GoodPub",
                    new decimal(8))
                );

            _unreleasedBooks.Add(new Book(
                    new Image(),
                    "Paper Towns",
                    "Sappy story",
                    "John Green",
                    "Drama",
                    "3214-76582",
                    "OneStory",
                    new decimal(12.50))
                );

            // RELEASED BOOKS
            _stocks.Add(new Stock(
                new Book(
                    new Image(),
                    "The Martian",
                    "Man goes to space, grows potatoes, does well for himself",
                    "Andy Weir",
                    "Adventure",
                    "6543-98765",
                    "Publishing Co.",
                    new decimal(6.99))
                )
                );

            _stocks.Add(new Stock(
                new Book(
                    new Image(),
                    "To Kill a Mockingbird",
                    "Good book",
                    "Harper Lee",
                    "Drama",
                    "5576-09876",
                    "NiceGuy",
                    new decimal(10.00))
                )
                );

            for (int i = 0; i < _stocks.Count; i++)
            {
                _store.AddBook(_stocks[i]);
                InStoreBox.Items.Add(_stocks[i].GetBook().GetTitle());
            }

            for (int i = 0; i < _unreleasedBooks.Count; i++)
            {
                NotInStoreBox.Items.Add(_unreleasedBooks[i].GetTitle());
            }
        }

        public void AddBookToStore(IBook book)
        {
            _store.AddBook(_stocks.First(s => s.GetBook().EqualsBook(book)));
        }

        private void UpdateQuantityButton_Click(object sender, RoutedEventArgs e)
        {
            // number to update the book by
            int updateNum = 0;

            // make sure a selection in the list has been made
            if (InStoreBox.SelectedIndex != -1)
            {
                // grab the user's input
                string updateString = Microsoft.VisualBasic.Interaction.InputBox("Please enter a number to update the selected book's quantity", 
                    _stocks[InStoreBox.SelectedIndex].GetBook().GetTitle(), "", -1, -1);
                try
                {
                    // try using the user's input as the update number
                    updateNum = Int32.Parse(updateString);
                }
                catch (Exception)
                {
                    Console.WriteLine("Not a valid integer");
                }

                // get the current book
                int i = InStoreBox.SelectedIndex;

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
        }
    }
}
