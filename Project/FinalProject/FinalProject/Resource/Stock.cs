namespace FinalProject
{
    public class Stock
    {
        private readonly IBook _book;
        private int _stock;
        private Store _store;
        private readonly StockLock _stockLock = new StockLock();

        public Stock(IBook book)
        {
            _book = book;
        }
        public Stock(IBook book, int initialquanitity)
        {
            _book = book;
            _stock = initialquanitity;
        }

        public void SetStore(Store s)
        {
            _store = s;
        }

        public IBook RemoveBook()
        {
            _stockLock.WriteLock();
            if (_stock <= 0) return null;
            _stock--;
            _stockLock.Done();
            return _book;
        }

        public void UpdateQuanitity(int amt)
        {
            _stockLock.WriteLock();
            _stock += amt;
            _stockLock.Done();
            _store.UpdateSubs(_book.Title);
        }

        public int GetQuantity()
        {
            _stockLock.ReadLock();
            var stock = _stock;
            _stockLock.Done();
            return stock;
        }

        public IBook GetBook()
        {
            return _book;
        }
    }
}
