using System;
using System.Collections.Generic;
using ChurchManager.Core.Domain;
using ChurchManager.Core.Persistence;
using ChurchManager.Core.Service;

namespace ChurchManager.Service
{
    public class PersonService : IPersonService
    {
        private readonly IRepository<Person> _personRepository;

        public PersonService(
            IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        public Person Save(Person person)
        {
            if (string.IsNullOrWhiteSpace(person.FirstName))
            {
                throw new ApplicationException("person_firstname_null");
            }
            if (string.IsNullOrWhiteSpace(person.LastName))
            {
                throw new ApplicationException("person_firstname_null");
            }

            if (_personRepository.Exists(p =>
                                         p.BirthDate == person.BirthDate
                                         && p.FirstName == person.FirstName
                                         && p.LastName == person.LastName))
            {
                throw new ApplicationException("person_already");   
            }

            _personRepository.Transaction(() => _personRepository.Save(person));
            return person;
        }

        public IList<Person> Find(string criteria)
        {
            criteria = criteria.ToLower();
            IList<Person> results = _personRepository.All(p =>
                                  p.FirstName.ToLower().Contains(criteria) || p.LastName.ToLower().Contains(criteria));
            return results;
        }
    }
}
