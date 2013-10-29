using System;
using ChurchManager.Core.Persistence;

namespace ChurchManager.Core.Domain
{
    public class Baptism : IEntity
    {
        public virtual int Id { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual Person Baptized { get; set; }

        public virtual Person GodParent1 { get; set; }

        public virtual Person GodParent2 { get; set; }
    }
}