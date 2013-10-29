using ChurchManager.Core.Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace ChurchManager.Persistence.Mappings
{
    public class PersonMap : ClassMapping<Person>
    {
        public PersonMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Identity));

            Property(x => x.BirthDate, m => m.NotNullable(true));
            Property(x => x.FirstName, m =>
                                           {
                                               m.NotNullable(true);
                                               m.Length(50);
                                           });
            Property(x => x.LastName, m =>
                                           {
                                               m.NotNullable(true);
                                               m.Length(50);
                                           });
        }
    }
}
