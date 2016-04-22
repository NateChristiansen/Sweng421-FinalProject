using System;

namespace FinalProject
{
    public class GenreFilter : ISearchFilter
    {
        private ISearchFilter _filter;

        public GenreFilter(ISearchFilter filter)
        {
            _filter = filter;
        }
        public void Apply(string filterText)
        {
            throw new NotImplementedException();
        }
    }
}