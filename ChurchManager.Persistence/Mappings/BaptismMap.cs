using ChurchManager.Core.Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace ChurchManager.Persistence.Mappings
{
    public class BaptismMap : ClassMapping<Baptism>
    {
        public BaptismMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Identity));

            Property(x => x.Date, m => m.NotNullable(true));

            ManyToOne(x => x.Baptized, m =>
                                             {
                                                 m.Fetch(FetchKind.Join);
                                                 m.NotNullable(true);
                                                 m.NotFound(NotFoundMode.Exception);
                                             });

            ManyToOne(x => x.GodParent1, m =>
                                             {
                                                 m.Fetch(FetchKind.Join);
                                                 m.NotNullable(true);
                                                 m.NotFound(NotFoundMode.Exception);
                                             });

            ManyToOne(x => x.GodParent2, m =>
                                             {
                                                 m.Fetch(FetchKind.Join);
                                                 m.NotNullable(true);
                                                 m.NotFound(NotFoundMode.Exception);
                                             });
        }
    }
}