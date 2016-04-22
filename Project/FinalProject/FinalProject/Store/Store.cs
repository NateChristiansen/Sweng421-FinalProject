using System.Collections.Generic;

namespace FinalProject
{
    public class Store
    {
        private List<IBook> BookList;

        public List<IBook> GetBooks()
        {
            return BookList;
        }

        public IBook PurchaseBook(List<string> titles, decimal wallet)
        {
            throw new System.NotImplementedException();
        }

        public List<IBook> Search(string searchQuery)
        {
            throw new System.NotImplementedException();
        }

        public void Subscribe(IMember subscriber, string title)
        {
            
        }
        public void UpdateSubs(string title)
        {
            
        }

        public void AddBook(IBook book)
        {
            
        }

    }
}