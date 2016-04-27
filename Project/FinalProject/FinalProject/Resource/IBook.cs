using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace FinalProject
{
    public interface IBook
    {
        string Title { get; }
        string Author { get; }
        string Genre { get; }
        string Publisher { get; }
        decimal Price { get; }
        BitmapImage Cover { get; }
        string Summary { get; }
        string Isbn { get; }
        bool EqualsBook(IBook b);
    }
}