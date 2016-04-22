namespace FinalProject
{
    public class Client
    {
        private readonly Store _store;
        private readonly ClientUi _ui;
        public Client(Store store)
        {
            _store = store;
            _ui = new ClientUi(this);
            _ui.Show();
        }
    }
}