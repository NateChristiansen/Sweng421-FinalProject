using System;
using System.Windows.Media.Imaging;

namespace FinalProject
{
    [Serializable]
    public class Magazine : AbstractBook
    {
        public int Issue { get; protected set; }

        public Magazine(BitmapImage cover, string title, string summary, string author, string genre,
            string isbn, string publisher, decimal price, int issue)
        {
            Cover = cover;
            Title = title;
            Author = author;
            Summary = summary;
            Genre = genre;
            Issue = issue;
            Isbn = isbn;
            Publisher = publisher;
            Price = price;
        }

        public int GetIssue()
        {
            return Issue;
        }
    }
}