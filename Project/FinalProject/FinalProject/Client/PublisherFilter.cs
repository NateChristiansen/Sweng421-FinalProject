using System;
using System.Collections.Generic;

namespace FinalProject
{
    public class PublisherFilter : ISearchFilter
    {
        private ISearchFilter _filter;

        public PublisherFilter(ISearchFilter filter)
        {
            _filter = filter;
        }

        public List<IBook> Apply(string filterText, List<IBook> list)
        {
            throw new NotImplementedException();
        }
    }
}