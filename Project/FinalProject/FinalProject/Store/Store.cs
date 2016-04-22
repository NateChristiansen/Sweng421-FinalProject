using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace FinalProject
{
    public class Store
    {
        private SubscriptionObserver subs;
        private Dictionary<string, Stock> Stocks;
        private BookFactory Factory;

        public List<IBook> GetBooks()
        {
            return Stocks.Values.Select(s => s.GetBook()).ToList();
        }

        public List<IBook> PurchaseBooks(List<string> titles, decimal wallet)
        {
            var l = new List<IBook>();
            titles.ForEach(t =>
            {
                if (Stocks[t].GetQuantity() > 0)
                {
                    l.Add(Factory.GetBook(t));
                }
            });
            return l;
        }

        public List<IBook> Search(string searchQuery)
        {
            var x = Regex.Split(searchQuery.Trim(), @"\s+").ToList();
            return GetBooks().Where(b => Contains(b, x)).ToList();
        }

        private bool Contains(IBook b, List<string> keywords)
        {
            if (!keywords.Any()) return Compare(b, keywords[0].ToLower());
            var keyword = keywords[0];
            keywords.RemoveAt(0);
            return Compare(b, keyword.ToLower()) && Contains(b, keywords);
        }

        private bool Compare(IBook b, string keyword)
        {
            return b.GetAuthor().ToLower().Contains(keyword) ||
                   b.GetGenre().ToLower().Contains(keyword) ||
                   b.GetPublisher().ToLower().Contains(keyword) ||
                   b.GetTitle().ToLower().Contains(keyword);
        }

        public void Subscribe(IMember subscriber, string title)
        {
            subs.AddSubscription(title, subscriber);
        }
        public void UpdateSubs(string title)
        {
            new Thread(() => subs.NotifySubscribers(title)).Start();
        }

        public void AddBook(Stock bookstock)
        {
            Stocks.Add(bookstock.GetBook().GetTitle(), bookstock);
        }
    }
}