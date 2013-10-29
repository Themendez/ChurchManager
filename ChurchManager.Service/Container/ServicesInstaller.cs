using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ChurchManager.Core.Service;

namespace ChurchManager.Service.Container
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //Specified repositories
            container.Register(
                Classes.FromAssemblyContaining<PersonService>()
                       .BasedOn<IApplicationService>()
                       .WithServiceDefaultInterfaces()
                       .LifestylePerWebRequest()
                );
        }
    }
}
