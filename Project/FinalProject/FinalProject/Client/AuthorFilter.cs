using System;

namespace FinalProject
{
    public class AuthorFilter : ISearchFilter
    {
        private ISearchFilter _filter;

        public AuthorFilter(ISearchFilter filter)
        {
            _filter = filter;
        }
        public void Apply(string filterText)
        {
            throw new NotImplementedException();
        }
    }
}