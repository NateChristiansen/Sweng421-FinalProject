using System;
using System.Windows.Controls;

namespace FinalProject
{
    public interface IBook
    {
        Image GetCover();
        string GetTitle();
        string GetSummary();
        string GetAuthor();
        string GetGenre();
        string GetISBN();
        string GetPublisher();
        Decimal GetPrice();

    }
}