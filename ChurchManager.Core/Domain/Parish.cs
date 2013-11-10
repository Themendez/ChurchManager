using System.Collections.Generic;
using ChurchManager.Core.Persistence;

namespace ChurchManager.Core.Domain
{
    public class Parish : IEntity
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }
        
        public virtual string Diocese { get; set; }

        public virtual string Street { get; set; }

        public virtual string Neighborhood { get; set; }

        public virtual int PostalCode { get; set; }

        public virtual List<Presbyter> Presbyters { get; set; }
    }
}