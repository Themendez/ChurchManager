using System;
using ChurchManager.Core.Domain;
using ChurchManager.Core.Persistence;
using ChurchManager.Core.Service;

namespace ChurchManager.Service.Services
{
    public class ParishService : IParishService
    {
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<Parish> _parishRepository;

        public ParishService(IRepository<Person> personRepository , IRepository<Parish> parishRepository )
        {
            _personRepository = personRepository;
            _parishRepository = parishRepository;
        }

        public Parish Save(Parish parish)
        {
            
            if (parish.Presbyters.Count == 0)
            {
                throw new ApplicationException("parish_presbyters_null");
            }
            _parishRepository.Transaction(() => _parishRepository.Save(parish));
            return parish;
        }
    }
}
