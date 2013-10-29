using System.Collections.Generic;
using ChurchManager.Core.Domain;

namespace ChurchManager.Core.Service
{
    public interface IPersonService : IApplicationService
    {
        Person Save(Person person);

        IList<Person> Find(string criteria);
    }
}
