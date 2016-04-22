using System;

namespace FinalProject
{
    public class BookFactory
    {
        public IBook GetBook(string bookTitle)
        {
            var type = Type.GetType("FinalProject." + bookTitle);
            return type != null ? (IBook)Activator.CreateInstance(type) : null;
        }
    }
}