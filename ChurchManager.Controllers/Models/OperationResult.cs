using System.Collections.Generic;

namespace ChurchManager.Controllers.Models
{
    public class OperationResult
    {
        public bool Success { get; set; }

        public object Data { get; set; }

        public IList<string> Errors { get; set; }
    }
}