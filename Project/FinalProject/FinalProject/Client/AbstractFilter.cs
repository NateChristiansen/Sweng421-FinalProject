using System;

namespace FinalProject
{
    public abstract class AbstractFilter : ISearchFilter
    {
        public void Apply(string filterText)
        {
            throw new NotImplementedException();
        }
    }
}