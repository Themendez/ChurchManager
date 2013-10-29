using AutoMapper;
using ChurchManager.Controllers.Models;
using ChurchManager.Core.Domain;

namespace ChurchManager.Controllers.Mappings
{
    public class BaptismMapping : IModelMapping
    {
        public void Map(IConfiguration cfg)
        {
            cfg.CreateMap<PersonModel, Person>();
            cfg.CreateMap<Person, PersonModel>();

            cfg.CreateMap<BaptismModel, Baptism>();
            cfg.CreateMap<Baptism, BaptismModel>();
        }
    }
}