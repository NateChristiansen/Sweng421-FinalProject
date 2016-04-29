using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject
{
    public class TitleFilter : ISearchFilter
    {
        private ISearchFilter _filter;

        public TitleFilter(ISearchFilter filter)
        {
            _filter = filter;
        }

        public List<IBook> Apply(string filterText, List<IBook> list)
        {
            return list.Where(b => b.Title.ToLower().Contains(filterText.ToLower())).ToList();
        }
    }
}