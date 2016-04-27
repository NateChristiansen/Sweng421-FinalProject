using System;

namespace FinalProject
{
    public class TitleFilter : ISearchFilter
    {
        private ISearchFilter _filter;

        public TitleFilter(ISearchFilter filter)
        {
            this._filter = filter;
        }

        public void Apply(string filterText)
        {
            throw new NotImplementedException();
        }
    }
}