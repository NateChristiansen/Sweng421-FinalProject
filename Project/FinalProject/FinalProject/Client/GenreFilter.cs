using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject
{
    public class GenreFilter : ISearchFilter
    {
        private ISearchFilter _filter;

        public GenreFilter(ISearchFilter filter)
        {
            _filter = filter;
        }

        public List<IBook> Apply(string filterText, List<IBook> list)
        {
            return list.Where(b => b.Genre.ToLower().Contains(filterText.ToLower())).ToList();
        }
    }
}