namespace FinalProject
{
    public class PublisherFilter : ISearchFilter
    {
        private ISearchFilter filter;

        public PublisherFilter(ISearchFilter filter)
        {
            this.filter = filter;
        }
        public void Apply(string filter)
        {
            throw new System.NotImplementedException();
        }
    }
}