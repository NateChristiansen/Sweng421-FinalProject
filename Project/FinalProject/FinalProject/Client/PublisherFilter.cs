using System;
using System.Collections.Generic;
using System.Linq;

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
            return list.Where(b => b.Publisher.ToLower().Contains(filterText.ToLower())).ToList();
        }
    }
}