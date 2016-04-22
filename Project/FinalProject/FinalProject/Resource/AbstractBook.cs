using System;
using System.Windows.Controls;

namespace FinalProject
{
    public abstract class AbstractBook : IBook
    {
        protected Image Cover;
        protected string Title;
        protected string Summary;
        protected string Author;
        protected string Genre;
        protected string ISBN;
        protected string Publisher;
        protected Decimal Price;
        protected int Stock;
        protected Store Store;

        public Image GetCover()
        {
            return Cover;
        }

        public string GetTitle()
        {
            return Title;
        }

        public string GetSummary()
        {
            return Summary;
        }

        public string GetAuthor()
        {
            return Author;
        }

        public string GetGenre()
        {
            return Genre;
        }

        public string GetISBN()
        {
            return ISBN;
        }

        public string GetPublisher()
        {
            return Publisher;
        }

        public decimal GetPrice()
        {
            return Price;
        }
    }
}