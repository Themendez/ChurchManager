using System;
using ChurchManager.Core.Domain;
using ChurchManager.Core.Persistence;
using ChurchManager.Core.Service;

namespace ChurchManager.Service.Services
{
    public class BaptismService : IBaptismService
    {
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<Baptism> _baptismRepository;

        public BaptismService(
            IRepository<Person> personRepository, 
            IRepository<Baptism> baptismRepository)
        {
            _personRepository = personRepository;
            _baptismRepository = baptismRepository;
        }

        public Baptism Save(Baptism baptism)
        {
            baptism.Baptized = _personRepository.Get(baptism.Baptized.Id);

            if (baptism.Baptized == null)
            {
                throw new ApplicationException("baptism_baptized_null");
            }

            if (_baptismRepository.Exists(b => b.Baptized.Id == baptism.Baptized.Id))
            {
                throw new ApplicationException("baptism_baptized_already");
            }

            baptism.GodParent1 = _personRepository.Get(baptism.GodParent1.Id);
            baptism.GodParent2 = _personRepository.Get(baptism.GodParent2.Id);

            if (baptism.GodParent1 == null || baptism.GodParent2 == null)
            {
                throw new ApplicationException("baptism_godparent_null");
            }

            if (baptism.Date <= DateTime.Today)
            {
                throw new ApplicationException("baptism_date_invalid");
            }

            _baptismRepository.Transaction(() => _baptismRepository.Save(baptism));
            return baptism;
        }
    }
}
