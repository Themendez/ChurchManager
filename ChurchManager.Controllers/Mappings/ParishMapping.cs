using AutoMapper;
using ChurchManager.Controllers.Controllers;
using ChurchManager.Controllers.Models;
using ChurchManager.Core.Domain;

namespace ChurchManager.Controllers.Mappings
{
    public class ParishMapping : IModelMapping
    {
        public void Map(IConfiguration cfg)
        {
            cfg.CreateMap<PersonModel, Person>();
            cfg.CreateMap<Person, PersonModel>();

            cfg.CreateMap<ParishModel, Parish>();
            cfg.CreateMap<Parish, ParishModel>();
        }
    }
}
