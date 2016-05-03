using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject
{
    public class AuthorFilter : ISearchFilter
    {
        private ISearchFilter _filter;

        public AuthorFilter(ISearchFilter filter)
        {
            _filter = filter;
        }

        public List<IBook> Apply(string filterText, List<IBook> list)
        {
            return list.Where(b => b.Author.ToLower().Contains(filterText.ToLower())).ToList();
        }
    }
}