using System.Collections.Generic;

namespace FinalProject
{
    public abstract class AbstractFilter : ISearchFilter
    {
        public abstract List<IBook> Apply(string filterText, List<IBook> list);
    }
}