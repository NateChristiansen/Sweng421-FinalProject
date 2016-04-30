using System;
using System.Collections.Generic;

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
            throw new NotImplementedException();
        }
    }
}