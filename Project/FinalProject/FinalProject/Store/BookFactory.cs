using System;

namespace FinalProject
{
    public class BookFactory
    {
        public IBook GetBook(string bookTitle)
        {
            Type type = Type.GetType("FinalProject." + bookTitle);
            return (IBook)Activator.CreateInstance(type);
        }
    }
}