using System;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using ChurchManager.Persistence.Mapping;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace ChurchManager.Persistence
{
    public class PersistenceManager
    {
        private static Configuration _configuration;
        private static ISessionFactory _sessionFactory;

        public static Configuration Configuration
        {
            get
            {
                return _configuration;
            }
        }

        public static ISessionFactory SessionFactory
        {
            get
            {
                return _sessionFactory;
            }
        }

        public static void Init(IWindsorContainer container)
        {
            ModelMapper mapper = new ModelMapper();
            foreach (Type type in typeof(PersonMap).Assembly.GetTypes().Where(t => !t.IsAbstract && t.IsClass))
            {
                if (IsMappingType(type))
                {
                    mapper.AddMapping(type);
                }
            }
            HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            _configuration = new Configuration().Configure();
            _configuration.AddMapping(domainMapping);
            _sessionFactory = _configuration.BuildSessionFactory();

            container.Register(
                Component.For<ISessionFactory>()
                         .Instance(SessionFactory)
                         .LifestyleSingleton()
                );
            container.Register(
                Component.For<ISession>()
                         .UsingFactoryMethod(k => k.Resolve<ISessionFactory>().OpenSession())
                         .LifestylePerWebRequest()
                         .OnDestroy(session =>
                                        {
                                            if (session != null)
                                            {
                                                if (session.Transaction.IsActive)
                                                {
                                                    session.Transaction.Rollback();
                                                }
                                                session.Close();
                                            }
                                        })
                );
        }

        private static bool IsMappingType(Type type)
        {
            bool result = false;
            if (type.BaseType != null)
            {
                result = type.BaseType.IsGenericType &&
                         (type.BaseType.GetGenericTypeDefinition() == typeof(ClassMapping<>)
                         || type.BaseType.GetGenericTypeDefinition() == typeof(SubclassMapping<>));
            }
            return result;
        }
    }
}
