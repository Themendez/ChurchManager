using System;
using ChurchManager.Core.Persistence;

namespace ChurchManager.Core.Domain
{
    public class Person : IEntity
    {
        public virtual int Id { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual DateTime BirthDate { get; set; }
    }
}