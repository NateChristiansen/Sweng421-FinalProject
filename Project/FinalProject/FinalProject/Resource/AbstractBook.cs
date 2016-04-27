using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace FinalProject
{
    public abstract class AbstractBook : IBook
    {
        public string Title { get; protected set; }
        public string Author { get; protected set; }
        public string Genre { get; protected set; }
        public string Publisher { get; protected set; }
        public decimal Price { get; protected set; }
        public BitmapImage Cover { get; protected set; }
        public string Summary { get; protected set; }
        public string Isbn { get; protected set; }

        public bool EqualsBook(IBook b)
        {
            return Title.Equals(b.Title);
        }
    }
}