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
            _store.AddBook(_stocks[0]);
        }

        public void AddBookToStore(IBook book)
        {
            _store.AddBook(_stocks.First(s => s.GetBook().EqualsBook(book)));
        }
    }
}