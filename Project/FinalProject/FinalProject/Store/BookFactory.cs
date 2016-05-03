using System.Collections.Generic;
using System.Linq;

namespace FinalProject
{
    public class BookFactory
    {
        private readonly Dictionary<IBook, Stock> _stocks = new Dictionary<IBook, Stock>(); 
        public IBook GetBook(IBook book)
        {
            if (!CheckStock(book)) return null;
            _stocks[book].RemoveBook();
            return book;
        }

        public List<IBook> GetBooks()
        {
            return _stocks.Keys.ToList();
        } 

        public bool CheckStock(IBook book)
        {
            if (!_stocks.ContainsKey(book)) return false;
            return _stocks[book].GetQuantity() > 0;
        }

        public void AddStock(Stock s)
        {
            if (_stocks.ContainsKey(s.GetBook())) return;
            _stocks.Add(s.GetBook(), s);
        }
    }
}