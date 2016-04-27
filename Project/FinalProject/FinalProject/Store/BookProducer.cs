using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace FinalProject
{
    public class BookProducer
    {
        private readonly Store _store;
        private readonly ProducerUi _ui;
        private readonly List<Stock> _stocks = new List<Stock>(); 

        public BookProducer(Store s)
        {
            _store = s;
            _ui = new ProducerUi(this);
            _ui.Show();
            InitUnreleasedBooks();
        }

        private void InitUnreleasedBooks()
        {
            _stocks.Add(new Stock(
                new Book(
                    new Image(),
                    "Harry Potter",
                    "Wizard boy wonder.",
                    "J.K. Rowling",
                    "Fantasy",
                    "1234-51234",
                    "Me.Awesome",
                    new decimal(10))
                )
                );
            //_store.AddBook(_stocks[0]);

            _stocks.Add(new Stock(
                new Book(
                    new Image(),
                    "The Hunger Games",
                    "Kids battle to the death.",
                    "Suzanne Collins",
                    "Action",
                    "4321-62145",
                    "GoodPub",
                    new decimal(8))
                )
                );

            _stocks.Add(new Stock(
                new Book(
                    new Image(),
                    "Paper Towns",
                    "Sappy story",
                    "John Green",
                    "Drama",
                    "3214-76582",
                    "OneStory",
                    new decimal(12.50))
                )
                );

            for (int i = 0; i < _stocks.Count; i++)
            {
                _store.AddBook(_stocks[i]);
                _ui.AddToUnreleasedBox(_stocks[i].GetBook());
            }


            
        }

        private void InitReleasedBooks()
        {
            
        }

        public void AddBookToStore(IBook book)
        {
            _store.AddBook(_stocks.First(s => s.GetBook().EqualsBook(book)));
        }
    }
}