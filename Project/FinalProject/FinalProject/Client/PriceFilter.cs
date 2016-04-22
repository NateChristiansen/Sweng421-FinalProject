using System;

namespace FinalProject
{
    public class PriceFilter : ISearchFilter
    {
        private ISearchFilter _filter;

        public PriceFilter(ISearchFilter filter)
        {
            this._filter = filter;
        }

        public void Apply(string filterText)
        {
            throw new NotImplementedException();
        }
    }
}