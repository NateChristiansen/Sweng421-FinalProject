namespace FinalProject
{
    public class AuthorFilter : ISearchFilter
    {
        private ISearchFilter filter;

        public AuthorFilter(ISearchFilter filter)
        {
            this.filter = filter;
        }
        public void Apply(string filter)
        {
            throw new System.NotImplementedException();
        }
    }
}