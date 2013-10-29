using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using ChurchManager.Controllers.Models;
using ChurchManager.Core.Domain;
using ChurchManager.Core.Service;

namespace ChurchManager.Controllers.Controllers
{
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(
            IPersonService personService)
        {
            _personService = personService;
        }

        public ActionResult Save(PersonModel model)
        {
            return ProcessRequest(
                () =>
                    {
                        Person person = Mapper.Map<PersonModel, Person>(model);
                        _personService.Save(person);
                        return Success();
                    });
        }

        public ActionResult Find(string criteria)
        {
            return ProcessRequest(
                () =>
                    {
                        IList<Person> results = _personService.Find(criteria);
                        IList<PersonModel> models = Mapper.Map<IList<Person>, IList<PersonModel>>(results);
                        return Success(models);
                    });
        }
    }
}
