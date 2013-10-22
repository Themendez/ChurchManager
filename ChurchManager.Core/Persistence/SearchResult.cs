using System.Collections.Generic;

namespace ChurchManager.Core.Persistence
{
    public class SearchResult<T>
    {
        public IList<T> List { get; set; }

        public int Total { get; set; }
    }
}