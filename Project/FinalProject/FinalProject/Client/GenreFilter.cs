namespace FinalProject
{
    public class GenreFilter : ISearchFilter
    {
        private ISearchFilter filter;

        public GenreFilter(ISearchFilter filter)
        {
            this.filter = filter;
        }
        public void Apply(string filter)
        {
            throw new System.NotImplementedException();
        }
    }
}