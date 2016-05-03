using System.Collections.Generic;

namespace FinalProject
{
    public interface ISearchFilter
    {
        List<IBook> Apply(string filterText, List<IBook> list);
    }
}