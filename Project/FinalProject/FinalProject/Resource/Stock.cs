using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class Stock
    {
        private IBook book;
        private int stock;
        private Store store;
        private StockLock StockLock;

        public void SetStore(Store s)
        {
            store = s;
        }

        public IBook RemoveBook()
        {
            if (stock <= 0) return null;
            stock--;
            return book;
        }

        public void UpdateQuanitity(int amt)
        {
            stock += amt;
        }

        public int GetQuantity()
        {
            return stock;
        }

        public IBook GetBook()
        {
            return book;
        }
    }
}
