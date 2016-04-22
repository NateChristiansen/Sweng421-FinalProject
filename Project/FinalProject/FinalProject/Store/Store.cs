using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace FinalProject
{
    public class Store
    {
        private readonly SubscriptionObserver _subs = new SubscriptionObserver();
        private readonly Dictionary<string, Stock> _stocks = new Dictionary<string, Stock>();
        private readonly BookFactory _factory = new BookFactory();

        public List<IBook> GetBooks()
        {
            return _stocks.Values.Select(s => s.GetBook()).ToList();
        }

        public List<IBook> PurchaseBooks(List<string> titles, decimal wallet)
        {
            var newBooks = new List<IBook>();
            titles.ForEach(t =>
            {
                if (_stocks[t].GetQuantity() > 0)
                {
                    newBooks.Add(_factory.GetBook(t));
                }
            });
            return newBooks;
        }

        public List<IBook> Search(string searchQuery)
        {
            var keywords = Regex.Split(searchQuery.Trim(), @"\s+").ToList();
            return GetBooks().Where(b => Contains(b, keywords)).ToList();
        }

        private static bool Contains(IBook b, IList<string> keywords)
        {
            if (!keywords.Any()) return Compare(b, keywords[0].ToLower());
            var keyword = keywords[0];
            keywords.RemoveAt(0);
            return Compare(b, keyword.ToLower()) && Contains(b, keywords);
        }

        private static bool Compare(IBook b, string keyword)
        {
            return b.GetAuthor().ToLower().Contains(keyword) ||
                   b.GetGenre().ToLower().Contains(keyword) ||
                   b.GetPublisher().ToLower().Contains(keyword) ||
                   b.GetTitle().ToLower().Contains(keyword);
        }

        public void Subscribe(IMember subscriber, string title)
        {
            _subs.AddSubscription(title, subscriber);
        }
        public void UpdateSubs(string title)
        {
            new Thread(() => _subs.NotifySubscribers(title)).Start();
        }

        public void AddBook(Stock bookstock)
        {
            _stocks.Add(bookstock.GetBook().GetTitle(), bookstock);
        }
    }
}