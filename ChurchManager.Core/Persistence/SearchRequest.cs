using System.Collections.Generic;

namespace ChurchManager.Core.Persistence
{
    public class SearchRequest
    {
        public int Start { get; set; }

        public int PageSize { get; set; }

        public bool Ascending { get; set; }

        public IList<string> SortFields { get; set; }
    }
}
