using System.Windows.Media.Imaging;

namespace FinalProject
{
    public class Book : AbstractBook
    {
        public Book(BitmapImage cover, string title, string summary, string author, string genre,
            string isbn, string publisher, decimal price)
        {
            Cover = cover;
            Title = title;
            Author = author;
            Summary = summary;
            Genre = genre;
            Isbn = isbn;
            Publisher = publisher;
            Price = price;
        }
    }
}