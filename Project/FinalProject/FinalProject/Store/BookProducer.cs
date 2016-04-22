namespace FinalProject
{
    public class BookProducer
    {
        private readonly Store _store;
        private readonly ProducerUi _ui;

        public BookProducer(Store s)
        {
            _store = s;
            _ui = new ProducerUi(this);
            _ui.Show();
        }
    }
}