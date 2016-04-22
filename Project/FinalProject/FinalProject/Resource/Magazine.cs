using System.Net.Mime;
using System.Windows.Controls;

namespace FinalProject
{
    public class Magazine : AbstractBook
    {
        protected int Issue;

        public Magazine(Image cover, string title, string summary, string author, string genre,
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