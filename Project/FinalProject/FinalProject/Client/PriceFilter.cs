namespace FinalProject
{
    public class PriceFilter : ISearchFilter
    {
        private ISearchFilter filter;

        public PriceFilter(ISearchFilter filter)
        {
            this.filter = filter;
        }

        public void Apply(string filter)
        {
            throw new System.NotImplementedException();
        }
    }
}