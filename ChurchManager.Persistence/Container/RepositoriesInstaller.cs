using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ChurchManager.Core.Persistence;
using ChurchManager.Persistence.Repositories;

namespace ChurchManager.Persistence.Container
{
    public class RepositoriesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            PersistenceManager.Init(container);

            //Default Implementation
            container.Register(
                Component.For(typeof (IRepository<>))
                         .ImplementedBy(typeof (RepositoryBase<>))
                         .LifestyleTransient()
                );

            //Specified repositories
            container.Register(
                Classes.FromAssembly(typeof (RepositoryBase<>).Assembly)
                       .BasedOn(typeof (IRepository<>))
                       .WithServiceDefaultInterfaces()
                       .LifestylePerWebRequest()
                );
        }
    }
}
