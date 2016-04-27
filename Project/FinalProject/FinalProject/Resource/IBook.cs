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
        string GetIsbn();
        string GetPublisher();
        decimal GetPrice();

        bool EqualsBook(IBook b);
    }
}