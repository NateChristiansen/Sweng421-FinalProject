using System;

namespace FinalProject
{
    public class PublisherFilter : ISearchFilter
    {
        private ISearchFilter _filter;

        public PublisherFilter(ISearchFilter filter)
        {
            _filter = filter;
        }
        public void Apply(string filterText)
        {
            throw new NotImplementedException();
        }
    }
}