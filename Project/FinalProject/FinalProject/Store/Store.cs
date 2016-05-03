using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace FinalProject
{
    public class Store
    {
        private readonly SubscriptionObserver _subs = new SubscriptionObserver();
        private readonly BookFactory _factory = new BookFactory();

        public List<IBook> GetBooks()
        {
            return _factory.GetBooks();
        }

        public void PurchaseBooks(List<IBook> books, Member user)
        {
            books.ForEach(b =>
            {
                var book = _factory.GetBook(b);
                if (book == null) return;
                user.OwnedBooks.Add(b);
                user.Wallet -= b.Price;
            });
        }

        public bool InStock(IBook book)
        {
            return _factory.CheckStock(book);
        }

        public List<IBook> Search(string searchQuery)
        {
            var keywords = Regex.Split(searchQuery.Trim(), @"\s+").ToList();
            return GetBooks().Where(b => Contains(b, keywords)).ToList();
        }

        private static bool Contains(IBook b, IList<string> keywords)
        {
            if (!keywords.Any()) return true;
            var keyword = keywords[0];
            keywords.RemoveAt(0);
            return Compare(b, keyword.ToLower()) && Contains(b, keywords);
        }

        private static bool Compare(IBook b, string keyword)
        {
            return b.Author.ToLower().Contains(keyword) ||
                   b.Genre.ToLower().Contains(keyword) ||
                   b.Publisher.ToLower().Contains(keyword) ||
                   b.Title.ToLower().Contains(keyword);
        }

        public void Subscribe(Member subscriber, IBook book)
        {
            _subs.AddSubscription(book, subscriber);
        }
        public void UpdateSubs(IBook book)
        {
            new Thread(() => _subs.NotifySubscribers(book)).Start();
        }

        public void AddBook(Stock bookstock)
        {
            _factory.AddStock(bookstock);
            bookstock.SetStore(this);
        }
    }
}